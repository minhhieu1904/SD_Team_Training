using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.SearchForOrderData;
using API.Helpers.Params.Transaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_SearchForOrderDataService : I_SearchForOrderDataServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;

        public S_SearchForOrderDataService(IRepositoryAccessor repositoryAccessor, IConfiguration configuration)
        {
            _repositoryAccessor = repositoryAccessor;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value;
        }

        public async Task<PaginationUtility<SearchForOrderDataViewModel>> GetDataPaing(PaginationParam pagination, SearchForOrderDataParam param)
        {
            var predicate = PredicateBuilder.New<MS_QR_Order>(x => x.cycle_flag == "N" && x.manuf == _manuf);
            if (!string.IsNullOrWhiteSpace(param.brandname) && param.brandname != "All")
                predicate.And(x => x.brandname.Trim() == param.brandname.Trim());
            if (!string.IsNullOrWhiteSpace(param.bitnbr))
                predicate.And(x => x.bitnbr.Trim() == param.bitnbr.Trim());
            if (!string.IsNullOrWhiteSpace(param.cusid))
                predicate.And(x => x.cusid.Trim() == param.cusid.Trim());
            if (!string.IsNullOrWhiteSpace(param.cusna))
                predicate.And(x => x.cusna.Trim() == param.cusna.Trim());
            if (!string.IsNullOrWhiteSpace(param.endcod) && param.endcod != "All")
                predicate.And(x => x.endcod.Trim() == param.endcod.Trim());
            if (!string.IsNullOrWhiteSpace(param.manno))
                predicate.And(x => x.manno.Trim() == param.manno.Trim());
            if (!string.IsNullOrWhiteSpace(param.rmodel))
                predicate.And(x => x.rmodel.Trim() == param.rmodel.Trim());
            if (!string.IsNullOrWhiteSpace(param.size))
                predicate.And(x => x.size.Trim() == param.size.Trim());
            if (!string.IsNullOrWhiteSpace(param.prtno))
                predicate.And(x => x.prtno.Trim() == param.prtno.Trim());
            if (!string.IsNullOrWhiteSpace(param.tolcls) && param.tolcls != "All")
                predicate.And(x => x.tolcls.Trim() == param.tolcls.Trim());
            predicate = param.is_remark ? predicate.And(x => x.remark != null) : predicate.And(x => x.remark == null);
            var data = _repositoryAccessor.MS_QR_Order.FindAll(predicate).OrderBy(x => x.eta).ThenBy(x => x.wkshqty).AsNoTracking().Project().To<SearchForOrderDataViewModel>();
            return await PaginationUtility<SearchForOrderDataViewModel>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<List<KeyValuePair<decimal, decimal>>> GetListPackage()
        {
            var data = await _repositoryAccessor.MS_Package.FindAll(x => x.Manuf == _manuf).Select(x => new KeyValuePair<decimal, decimal>(x.PackageQty, x.PackageQty)).Distinct().ToListAsync();
            return data.OrderBy(x => x.Value).ToList();
        }

        public async Task<OperationResult> OrderPrint(OrderDataPrint dataPrint)
        {
            var labelLast = await _repositoryAccessor.MS_QR_Label.FindAll().OrderBy(x => x.CrDay).ThenBy(x => x.QRCodeID).LastOrDefaultAsync();
            string QRCodeIDDay = DateTime.Now.ToString("yyyyMMdd");
            string code = (labelLast == null || labelLast.QRCodeID.Substring(0, 8) != QRCodeIDDay) ? "00001" : GenerateCodeUtility.Identity(labelLast.QRCodeID.Substring(8));
            if (dataPrint.Is_Remark && dataPrint.Items.Count() == 1)
                return await OrderPrintRemark(dataPrint, QRCodeIDDay, code);
            else
            {
                if (dataPrint.Balance)
                    return await OrderPrintIsBalance(dataPrint, QRCodeIDDay, code);
                else
                    return await OrderPrintIsNonBalance(dataPrint, QRCodeIDDay, code);
            }
        }


        #region OrderPrintIsBalance -> Balance = true
        private async Task<OperationResult> OrderPrintIsBalance(OrderDataPrint dataPrint, string QrcodeId, string code)
        {
            List<OrderPrintResult> printResults = new();
            decimal canPrintQty = 0;
            decimal currentPrintQty = 0;

            using var transaction = await _repositoryAccessor.BeginTransactionAsync();
            foreach (var item in dataPrint.Items)
            {
                currentPrintQty = Math.Min(item.max_qty_can_print, dataPrint.PrintQty);
                //th 3 tick 2 balance
                var compare = await Compare_Qty(item, currentPrintQty);
                if (compare.IsSuccess)
                {
                    var data = await GetQROrderRecord(item);

                    if (data == null)
                    {
                        await transaction.RollbackAsync();
                        return new OperationResult() { IsSuccess = false };
                    }
                    short tempSerial = await GetSerial(item);
                    canPrintQty = Math.Min(item.max_qty_can_print, dataPrint.PrintQty);

                    while (canPrintQty > 0)
                    {
                        var orderQty = Math.Min(Convert.ToInt32(canPrintQty), dataPrint.PackageQty);
                        item.max_qty_can_print -= orderQty;
                        canPrintQty -= orderQty;
                        dataPrint.PrintQty -= (int)orderQty;

                        var serial = ++tempSerial;
                        OrderPrintResult orderData = await OrderPrintData(data, orderQty, serial, dataPrint);

                        MS_QR_Label newLabel = await LabelData(data, orderData, dataPrint, $"{QrcodeId}{code}");

                        _repositoryAccessor.MS_QR_Label.Add(newLabel);
                        await _repositoryAccessor.Save();
                        orderData.qrCodeName = newLabel.QRCodeValue;

                        data.pqty += orderData.qty;
                        printResults.Add(orderData);

                        code = GenerateCodeUtility.Identity(code);
                    }

                    data.update_time = DateTime.Now;
                    data.updated_by = dataPrint.UserName;

                    _repositoryAccessor.MS_QR_Order.Update(data);
                    await _repositoryAccessor.Save();
                }
                else
                {
                    await transaction.RollbackAsync();
                    return new OperationResult()
                    {
                        IsSuccess = false,
                        Error = "QuantityExceeeded",
                        Data = compare.Data
                    };
                }
            }
            try
            {
                await transaction.CommitAsync();
                return new OperationResult() { IsSuccess = true, Data = printResults };
            }
            catch
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
        }
        #endregion

        #region OrderPrintIsNonBalance -> Balance = false
        private async Task<OperationResult> OrderPrintIsNonBalance(OrderDataPrint dataPrint, string QrcodeId, string code)
        {
            List<OrderPrintResult> printResults = new();
            decimal canPrintQty = 0;
            decimal printQty = dataPrint.PrintQty;
            decimal currentPrintQty = 0;

            using var transaction = await _repositoryAccessor.BeginTransactionAsync();
            foreach (var item in dataPrint.Items)
            {
                currentPrintQty = Math.Min(item.wkshqty, printQty);
                //th 3 khong tick balance
                var compare = await Compare_Qty(item, currentPrintQty);
                if (compare.IsSuccess)
                {
                    var data = await GetQROrderRecord(item);
                    if (data == null)
                    {
                        await transaction.RollbackAsync();
                        return new OperationResult() { IsSuccess = false };
                    }

                    short tempSerial = await GetSerial(item);
                    canPrintQty = Math.Min(item.max_qty_can_print, dataPrint.PrintQty);

                    while (canPrintQty > 0)
                    {
                        var orderQty = Math.Min(Convert.ToInt32(canPrintQty), dataPrint.PackageQty);
                        item.max_qty_can_print -= orderQty;
                        canPrintQty -= orderQty;
                        dataPrint.PrintQty -= (int)orderQty;

                        if (orderQty == dataPrint.PrintQty)
                        {
                            var serial = ++tempSerial;
                            OrderPrintResult orderData = await OrderPrintData(data, orderQty, serial, dataPrint);

                            MS_QR_Label newLabel = await LabelData(data, orderData, dataPrint, $"{QrcodeId}{code}");

                            _repositoryAccessor.MS_QR_Label.Add(newLabel);
                            await _repositoryAccessor.Save();
                            orderData.qrCodeName = newLabel.QRCodeValue;

                            data.qty += orderData.qty;

                            printResults.Add(orderData);
                            code = GenerateCodeUtility.Identity(code);
                        }
                    }

                    data.update_time = DateTime.Now;
                    data.updated_by = dataPrint.UserName;

                    _repositoryAccessor.MS_QR_Order.Update(data);
                    await _repositoryAccessor.Save();
                }
                else
                {
                    await transaction.RollbackAsync();
                    return new OperationResult() { IsSuccess = false, Error = "QuantityExceeded", Data = compare.Data };
                }

                printQty = printQty - item.wkshqty;
            }
            try
            {
                await transaction.CommitAsync();
                return new OperationResult()
                { IsSuccess = true, Data = printResults };
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
        }
        #endregion

        #region  Compare_Qty
        private async Task<OperationResult> Compare_Qty(OrderDataItem item, decimal currentPrintQty)
        {
            var listOrder = await _repositoryAccessor.MS_QR_Order
            .FindAll(x => x.purno.Trim() == item.purno.Trim() && x.manno.Trim() == item.manno.Trim() && x.size.Trim() == item.size.Trim())
            .AsNoTracking().ToListAsync();

            decimal sumqty = listOrder.Sum(x => x.pqty);
            decimal qty = listOrder.Where(x => x.kind.Trim() != "5").OrderByDescending(x => x.mdat).ThenByDescending(x => x.wkshno).FirstOrDefault()?.qty ?? 0;

            return new OperationResult
            {
                IsSuccess = qty >= (sumqty + currentPrintQty),
                Data = new CompareQtyResult { qty = qty, pqty = sumqty, printQty = currentPrintQty }
            };
        }
        #endregion

        #region OrderPrintRemark
        private async Task<OperationResult> OrderPrintRemark(OrderDataPrint dataPrint, string QrcodeId, string code)
        {
            //TH1
            List<OrderPrintResult> printResults = new();
            var item = dataPrint.Items[0];

            using var transaction = await _repositoryAccessor.BeginTransactionAsync();
            var data = await GetQROrderRecord(item);

            if (data == null)
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
            short tempSerial = await GetSerial(item);
            OrderPrintResult orderData = await OrderPrintData(data, dataPrint.PrintQty, ++tempSerial, dataPrint);

            //Them du lieu vao MS_QR_Label
            MS_QR_Label newLabel = await LabelData(data, orderData, dataPrint, $"{QrcodeId}{code}");
            _repositoryAccessor.MS_QR_Label.Add(newLabel);
            await _repositoryAccessor.Save();

            orderData.qrCodeName = newLabel.QRCodeValue;
            printResults.Add(orderData);

            code = GenerateCodeUtility.Identity(code);

            data.pqty = dataPrint.PrintQty;
            data.update_time = DateTime.Now;
            data.updated_by = dataPrint.UserName;
            _repositoryAccessor.MS_QR_Order.Update(data);

            await _repositoryAccessor.Save();
            try
            {
                await transaction.CommitAsync();
                return new OperationResult() { IsSuccess = true, Data = printResults };
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
        }
        #endregion

        #region GetQROrderRecord
        private async Task<MS_QR_Order> GetQROrderRecord(OrderDataItem item)
        {
            return await _repositoryAccessor.MS_QR_Order.
            FirstOrDefaultAsync(x => x.purno.Trim() == item.purno.Trim() &&
            x.manno.Trim() == item.manno.Trim() &&
            x.size.Trim() == item.size.Trim() &&
            x.wkshno.Trim() == item.wkshno.Trim() &&
            x.prtno.Trim() == item.prtno.Trim()
           );

        }
        #endregion 

        #region GetSerial
        private async Task<short> GetSerial(OrderDataItem item)
        {
            var ms_Qr_Label = await _repositoryAccessor.MS_QR_Label.FindAll(
                x => x.PurNo.Trim() == item.purno.Trim() &&
                x.ManNo.Trim() == item.manno.Trim() &&
                x.Size.Trim() == item.size.Trim()
            ).OrderBy(x => x.Serial).LastOrDefaultAsync();
            return ms_Qr_Label == null ? Convert.ToInt16(0) : ms_Qr_Label.Serial;
        }
        #endregion

        #region OrderPrintData
        private async Task<OrderPrintResult> OrderPrintData(MS_QR_Order data, decimal orderQty, short serial, OrderDataPrint dataPrint)
        {
            OrderPrintResult orderData = new OrderPrintResult()
            {
                purno = data.purno,
                rmodel = data.rmodel,
                manno = data.manno,
                article = data.article,
                size = data.size,
                bitnbr = data.bitnbr,
                custid = data.cusid,
                kind = data.kind,
                qty = data.qty,
                serial = serial,
                prtno = data.prtno,
                wkshno = data.wkshno,
                update_by = dataPrint.UserName,
                remark = data.remark
            };
            return await Task.FromResult(orderData);
        }
        #endregion

        #region LabelData
        private async Task<MS_QR_Label> LabelData(MS_QR_Order data, OrderPrintResult orderData, OrderDataPrint dataPrint, string QrcodeId)
        {
            MS_QR_Label newLabel = new()
            {
                Manuf = data.manuf,
                Type = "A",
                ManNo = data.manno,
                PurNo = data.purno,
                Serial = orderData.serial,
                Size = data.size,
                Qty = orderData.qty,
                Prt_Cnt = 1,
                Flag = "Y",
                CrUsr = dataPrint.UserName,
                CrDay = DateTime.Now,
                PrtDay = DateTime.Now,
                wkshno = data.wkshno,
                prtno = data.prtno,
                empno = dataPrint.Empno,
                Grade = dataPrint.Grade,
                QRCodeID = QrcodeId
            };
            return await Task.FromResult(newLabel);
        }
        #endregion
    }
}