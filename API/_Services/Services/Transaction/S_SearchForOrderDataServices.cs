
using API._Repositories;
using API._Services.Interfaces.Transaction;
using API.DTOs;
using API.DTOs.Trainsaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Transaction
{
    public class S_SearchForOrderDataServices : I_SearchForOrderDataServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;
        FunctionUtility _functionUtility = new FunctionUtility();

        public S_SearchForOrderDataServices(IRepositoryAccessor repositoryAccessor, IConfiguration configuration)
        {
            _repositoryAccessor = repositoryAccessor;
            _configuration = configuration;
        }

        public async Task<PaginationUtility<SearchForOrderDataDTO>> GetDataPagination(PaginationParam pagination, SearchForOrderDataParam param)
        {
            var pred = PredicateBuilder.New<MsQrOrder>(x => x.CycleFlag == "N" && x.Manuf == "N");

            if (!string.IsNullOrEmpty(param.brandname))
                pred.And(x => x.Brandname.Trim() == param.brandname.Trim() && param.brandname != "All");

            if (!string.IsNullOrEmpty(param.rmodel))
                pred.And(x => x.Rmodel.Trim() == param.rmodel.Trim());

            if (!string.IsNullOrEmpty(param.bitnbr))
                pred.And(x => x.Bitnbr.Trim() == param.bitnbr.Trim());

            if (!string.IsNullOrEmpty(param.size))
                pred.And(x => x.Size.Trim() == param.size.Trim());

            if (!string.IsNullOrEmpty(param.cusid))
                pred.And(x => x.Cusid.Trim() == param.cusid.Trim());

            if (!string.IsNullOrEmpty(param.manno))
                pred.And(x => x.Manno.Trim() == param.manno.Trim());

            if (!string.IsNullOrEmpty(param.endcod))
                pred.And(x => x.Endcod.Trim() == param.endcod.Trim() && param.endcod != "All");

            if (!string.IsNullOrEmpty(param.cusna))
                pred.And(x => x.Cusna.Trim() == param.cusna.Trim());

            if (!string.IsNullOrEmpty(param.prtno))
                pred.And(x => x.Prtno.Trim() == param.prtno.Trim());
            pred = param.remark ? pred.And(x => x.Kind.Trim() == "4" && x.Remark != null) : pred.And(x => x.Remark == null);
            var data = await _repositoryAccessor.MS_QrOrder.FindAll(pred).Select(x => new SearchForOrderDataDTO
            {
                brandname = x.Brandname,
                rmodel = x.Rmodel,
                bitnbr = x.Bitnbr,
                size = x.Size,
                cusid = x.Cusid,
                manno = x.Manno,
                purno = x.Purno,
                endcod = x.Endcod,
                wkshno = x.Wkshno,
                cusna = x.Cusna,
                kind = x.Kind,
                eta = x.Eta,
                article = x.Article,
                qty = x.Qty,
                wkshqty = x.Wkshqty,
                addqty = x.Addqty,
                lessqty = x.Lessqty,
                apqty = x.Qty + x.Addqty - x.Lessqty - x.Pqty,
                pqty = x.Pqty,
                cqty = x.Cqty,
                ritnbr = x.Ritnbr,
                style = x.Style,
                tsize = x.Tsize,
                remark = x.Remark,
                prtno = x.Prtno,
                update_time = x.UpdateTime,
                updated_by = x.UpdatedBy,
                isDisabled = x.Pqty >= x.Wkshqty
            }).OrderBy(x => x.eta).ThenBy(x => x.wkshqty).ToListAsync();

            return PaginationUtility<SearchForOrderDataDTO>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<List<KeyValuePair<string, string>>> GetListBrandName()
        {
            return await _repositoryAccessor.MS_QrOrder.FindAll()
                    .Select(x => new KeyValuePair<string, string>(x.Brandname, x.Brandname)).Distinct().ToListAsync();
        }

        public async Task<List<KeyValuePair<decimal, decimal>>> GetListPackage()
        {
            var data = await _repositoryAccessor.MS_Package.FindAll()
                    .Select(x => new KeyValuePair<decimal, decimal>(x.PackageQty, x.PackageQty)).Distinct().ToListAsync();
            return data.OrderBy(x => x.Value).ToList();
        }

        public async Task<List<KeyValuePair<string, string>>> GetListStatus()
        {
            var data = await _repositoryAccessor.MS_QrOrder.FindAll()
                    .Select(x => new KeyValuePair<string, string>(x.Endcod, x.Endcod)).Distinct().ToListAsync();
            return data.OrderByDescending(x => x.Value).ToList();
        }

        public async Task<OperationResult> Print(OrderDataPrint dataPrint)
        {
            var labelLast = await _repositoryAccessor.MS_QrLabel.FindAll().OrderBy(x => x.CrDay).ThenBy(x => x.QRCodeID).LastOrDefaultAsync();
            string QRCodeIDDay = DateTime.Now.ToString("yyyyMMdd");
            string code = (labelLast == null || labelLast.QRCodeID.Substring(0, 8) != QRCodeIDDay)
                                                    ? "00001" : _functionUtility.GenerateCodeIdentity(labelLast.QRCodeID.Substring(8));
            // Kiểm tra TH 1 isRemark 
            if (dataPrint.Remark && dataPrint.Items.Count() == 1)
                return await OrderPrintRemark(dataPrint, QRCodeIDDay, code);
            else
            {
                // Kiểm tra TH 2 & 3
                if (dataPrint.Balance)
                    return await OrderPrintIsBalance(dataPrint, QRCodeIDDay, code);
                else
                    return await OrderPrintIsNonBalance(dataPrint, QRCodeIDDay, code);
            }
        }

        private async Task<OperationResult> OrderPrintRemark(OrderDataPrint dataPrint, string QRCodeIDDay, string code)
        {
            // TH 1
            List<OrderPrintResult> printDataResult = new();
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
            // Khởi tạo dữ liệu kiểu MsQrLabel và thêm vào Table MS_QR_Label
            MsQrLabel newLabel = await LabelData(data, orderData, dataPrint, $"{QRCodeIDDay}{code}");

            _repositoryAccessor.MS_QrLabel.Add(newLabel);
            await _repositoryAccessor.Save();

            orderData.qrCodeName = newLabel.QRCodeValue;
            printDataResult.Add(orderData);

            FunctionUtility function = new FunctionUtility();
            code = function.GenerateCodeIdentity(code);
            // Cập nhật thông tin Table MS_QR_Order
            data.Pqty = dataPrint.PrintQty;
            data.UpdateTime = DateTime.Now;
            data.UpdatedBy = dataPrint.UserName;
            _repositoryAccessor.MS_QrOrder.Update(data);

            await _repositoryAccessor.Save();
            try
            {
                await transaction.CommitAsync();
                return new OperationResult() { IsSuccess = true, Data = printDataResult };
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
        }

        private async Task<OperationResult> OrderPrintIsBalance(OrderDataPrint dataPrint, string QRCodeIDDay, string code)
        {
            List<OrderPrintResult> printDataResult = new();
            decimal canPrintQty = 0;
            decimal currentPrintQty = 0;

            using var transaction = await _repositoryAccessor.BeginTransactionAsync();
            foreach (var item in dataPrint.Items)
            {
                currentPrintQty = Math.Min(item.maxPqty, dataPrint.PrintQty);
                // TH 3 DK 2 Balance Tick
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
                    canPrintQty = Math.Min(item.maxPqty, dataPrint.PrintQty);

                    while (canPrintQty > 0)
                    {
                        var orderQty = Math.Min(Convert.ToInt32(canPrintQty), dataPrint.PackageQty);
                        item.maxPqty -= orderQty;
                        canPrintQty -= orderQty;
                        dataPrint.PrintQty -= (int)orderQty;

                        var serial = ++tempSerial;
                        OrderPrintResult orderData = await OrderPrintData(data, orderQty, serial, dataPrint);

                        MsQrLabel newLabel = await LabelData(data, orderData, dataPrint, $"{QRCodeIDDay}{code}");

                        _repositoryAccessor.MS_QrLabel.Add(newLabel);
                        await _repositoryAccessor.Save();
                        orderData.qrCodeName = newLabel.QRCodeValue;

                        data.Pqty += orderData.qty;
                        printDataResult.Add(orderData);

                        FunctionUtility function = new FunctionUtility();
                        code = _functionUtility.GenerateCodeIdentity(code);
                    }

                    data.UpdateTime = DateTime.Now;
                    data.UpdatedBy = dataPrint.UserName;

                    _repositoryAccessor.MS_QrOrder.Update(data);
                    await _repositoryAccessor.Save();
                }
                else
                {
                    await transaction.RollbackAsync();
                    return new OperationResult() { IsSuccess = false, Error = "QuantityExceeded", Data = compare.Data };
                }
            }
            try
            {
                await transaction.CommitAsync();
                return new OperationResult() { IsSuccess = true, Data = printDataResult };
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
        }

        private async Task<OperationResult> OrderPrintIsNonBalance(OrderDataPrint dataPrint, string QRCodeIDDay, string code)
        {
            List<OrderPrintResult> printDataResult = new();
            decimal canPrintQty = 0;
            decimal printQty = dataPrint.PrintQty;
            decimal currentPrintQty = 0;

            using var transaction = await _repositoryAccessor.BeginTransactionAsync();
            foreach (var item in dataPrint.Items)
            {
                currentPrintQty = Math.Min(item.wkshqty, printQty);
                // TH 3 DK 3 Balane NOT Tick
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
                    canPrintQty = Math.Min(item.maxPqty, dataPrint.PrintQty);
                    // Chạy tới khi không còn Print đc
                    while (canPrintQty > 0)
                    {
                        var orderQty = Math.Min(Convert.ToInt32(canPrintQty), dataPrint.PackageQty);
                        item.maxPqty -= orderQty;
                        canPrintQty -= orderQty;
                        dataPrint.PrintQty -= (int)orderQty;

                        if (orderQty == dataPrint.PackageQty)
                        {
                            var serial = ++tempSerial;
                            OrderPrintResult orderData = await OrderPrintData(data, orderQty, serial, dataPrint);

                            MsQrLabel newLabel = await LabelData(data, orderData, dataPrint, $"{QRCodeIDDay}{code}");

                            _repositoryAccessor.MS_QrLabel.Add(newLabel);
                            await _repositoryAccessor.Save();
                            orderData.qrCodeName = newLabel.QRCodeValue;

                            data.Pqty += orderData.qty;
                            printDataResult.Add(orderData);

                            code = _functionUtility.GenerateCodeIdentity(code);
                        }
                    }

                    data.UpdateTime = DateTime.Now;
                    data.UpdatedBy = dataPrint.UserName;

                    _repositoryAccessor.MS_QrOrder.Update(data);
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
                return new OperationResult() { IsSuccess = true, Data = printDataResult };
            }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
        }

        private async Task<OperationResult> Compare_Qty(OrderDataItem item, decimal currentPrintQty)
        {

            var listOrder = await _repositoryAccessor.MS_QrOrder
                .FindAll(x => x.Purno.Trim() == item.purno.Trim() && x.Manno.Trim() == item.manno.Trim() && x.Size.Trim() == item.size.Trim())
                .AsNoTracking().ToListAsync();

            decimal sumpqty = listOrder.Sum(x => x.Pqty);
            decimal qty = listOrder.Where(x => x.Kind.Trim() != "5").OrderByDescending(x => x.Mdat).ThenByDescending(x => x.Wkshno).FirstOrDefault()?.Qty ?? 0;

            return new OperationResult
            {
                IsSuccess = qty >= (sumpqty + currentPrintQty),
                Data = new CompareQtyResult { qty = qty, pqty = sumpqty, printQty = currentPrintQty }
            };
        }

        private async Task<MsQrOrder> GetQROrderRecord(OrderDataItem item)
        {
            return await _repositoryAccessor.MS_QrOrder
                .FirstOrDefaultAsync(x => x.Purno.Trim() == item.purno.Trim() &&
                    x.Manno.Trim() == item.manno.Trim() &&
                    x.Size.Trim() == item.size.Trim() &&
                    x.Wkshno.Trim() == item.wkshno.Trim() &&
                    x.Prtno.Trim() == item.prtno.Trim());
        }

        private async Task<short> GetSerial(OrderDataItem item)
        {
            var mS_QR_Label = await _repositoryAccessor.MS_QrLabel
                .FindAll(
                    x => x.PurNo.Trim() == item.purno.Trim() &&
                    x.ManNo.Trim() == item.manno.Trim() &&
                    x.Size.Trim() == item.size.Trim()
                ).OrderBy(x => x.Serial).LastOrDefaultAsync();
            return mS_QR_Label == null ? Convert.ToInt16(0) : mS_QR_Label.Serial;
        }

        private async Task<OrderPrintResult> OrderPrintData(MsQrOrder data, decimal orderQty, short serial, OrderDataPrint dataPrint)
        {
            OrderPrintResult orderData = new OrderPrintResult()
            {
                purno = data.Purno,
                rmodel = data.Rmodel,
                manno = data.Manno,
                article = data.Article,
                size = data.Size,
                bitnbr = data.Bitnbr,
                custid = data.Cusid,
                kind = data.Kind,
                qty = orderQty,
                serial = serial,
                prtno = data.Prtno,
                wkshno = data.Wkshno,
                update_by = dataPrint.UserName,
                remark = data.Remark
            };
            return await Task.FromResult(orderData);
        }

        private async Task<MsQrLabel> LabelData(MsQrOrder data, OrderPrintResult orderData, OrderDataPrint dataPrint, string QRCodeID)
        {
            MsQrLabel newLabel = new()
            {
                Manuf = data.Manuf,
                Type = "A",
                ManNo = data.Manno,
                PurNo = data.Purno,
                Serial = orderData.serial,
                Size = data.Size,
                Qty = orderData.qty,
                Prt_Cnt = 1,
                Flag = "Y",
                CrUsr = dataPrint.UserName,
                CrDay = DateTime.Now,
                PrtDay = DateTime.Now,
                wkshno = data.Wkshno,
                prtno = data.Prtno,
                empno = dataPrint.Empno,
                Grade = dataPrint.Grade,
                QRCodeID = QRCodeID
            };
            return await Task.FromResult(newLabel);
        }
    }
}