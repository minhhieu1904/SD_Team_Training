
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces.Transaction.SearchForOderData;
using API.DTOs.Transaction.SearchForOrderData;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Transaction.SearchForOrderData
{
    public class SearchForOrderDataService : ISearchForOrderDataService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;
        private readonly IFunctionUtility _functionUtility;

        public SearchForOrderDataService(IRepositoryAccessor repository, IConfiguration configuration, IFunctionUtility functionUtility)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value; 
            _functionUtility = functionUtility;
        }
        public async Task<List<KeyValuePair<string, string>>> GetListBrandname()
        {
            return await _repository.MS_QR_Order.FindAll().Select(x => new KeyValuePair<string, string>(x.brandname, x.brandname)).Distinct().ToListAsync();
        }

        public async Task<PaginationUtility<SearchForOrderDataViewModel>> GetDataPagination(PaginationParam pagination, SearchForOrderDataParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<MS_QR_Order>(x => x.cycle_flag == "N" && x.manuf == _manuf);
            if (!string.IsNullOrWhiteSpace(param.brandname) && param.brandname != "ALL")
                predicate.And(x => x.brandname.Trim() == param.brandname.Trim());
            if (!string.IsNullOrWhiteSpace(param.bitnbr))
                predicate.And(x => x.bitnbr.Trim() == param.bitnbr.Trim());
            if (!string.IsNullOrWhiteSpace(param.cusid))
                predicate.And(x => x.cusid.Trim() == param.cusid.Trim());
            if (!string.IsNullOrWhiteSpace(param.cusna))
                predicate.And(x => x.cusna.Trim() == param.cusna.Trim());
            if (!string.IsNullOrWhiteSpace(param.endcod))
                predicate.And(x => x.endcod.Trim() == param.endcod.Trim());
            if (!string.IsNullOrWhiteSpace(param.manno))
                predicate.And(x => x.manno.Trim() == param.manno.Trim());
            if (!string.IsNullOrWhiteSpace(param.rmodel))
                predicate.And(x => x.rmodel.Trim() == param.rmodel.Trim());
            if (!string.IsNullOrWhiteSpace(param.size))
                predicate.And(x => x.size.Trim() == param.size.Trim());
            if (!string.IsNullOrWhiteSpace(param.prtno))
                predicate.And(x => x.prtno.Trim() == param.prtno.Trim());
            predicate = param.remark ? predicate.And(x => x.kind.Trim() == "4" && x.remark != null) : predicate.And(x => x.remark == null);
            var data = _repository.MS_QR_Order.FindAll(predicate).OrderBy(x => x.eta).ThenBy(x => x.wkshqty).AsNoTracking().Project().To<SearchForOrderDataViewModel>();
            return await PaginationUtility<SearchForOrderDataViewModel>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }
        #region GetListPackage
        public async Task<List<KeyValuePair<decimal, decimal>>> GetListPackage()
        {
            var data = await _repository.MS_Package.FindAll(x => x.Manuf == _manuf).Select(x => new KeyValuePair<decimal, decimal>(x.PackageQty, x.PackageQty)).Distinct().ToListAsync();
            return data.OrderBy(x => x.Value).ToList();
        }

        #endregion
        #region OrderPrint
        public async Task<OperationResult> OrderPrint(OrderDataPrint dataPrint)
        {
           var labelLast = await _repository.MS_QR_Label.FindAll().OrderBy(x => x.CrDay).ThenBy(x => x.QRCodeID).LastOrDefaultAsync();
           string QRCodeIDDay = DateTime.Now.ToString("yyyyMMdd");
           string code = (labelLast == null || labelLast.QRCodeID.Substring(0,8) != QRCodeIDDay) ? "00001" : _functionUtility.GenerateCodeIdentity(labelLast.QRCodeID.Substring(8));
           if(dataPrint.Is_Remark &&  dataPrint.Items.Count() == 1)
            return await OrderPrintRemark(dataPrint, QRCodeIDDay, code);
          else
            {
                if (dataPrint.Balance)
                    return await OrderPrintIsBalance(dataPrint, QRCodeIDDay, code);
                else
                    return await OrderPrintIsNonBalance(dataPrint, QRCodeIDDay, code);
            }
        }
        #endregion
        #region orderPrintRemark
        private async Task<OperationResult> OrderPrintRemark(OrderDataPrint dataPrint, string QRCodeIDDay, string code)
        {
            List<OrderPrintParam> printDataParam = new ();
            var item = dataPrint.Items[0];
            using var transaction = await _repository.BeginTransactionAsync();
            var data = await GetQROrderRecord(item);
            if(data == null)
            {
                await transaction.RollbackAsync();
                return new OperationResult() {IsSuccess =false};
            }
            short tempSerial = await GetSerial(item);
            OrderPrintParam orderData = await OrderPrintData(data,dataPrint.PrintQty, ++tempSerial,dataPrint);
            MS_QR_Label newLabel = await LabelData(data,orderData, dataPrint, $"{QRCodeIDDay}{code}");
            _repository.MS_QR_Label.Add(newLabel);
            await _repository.Save();
            orderData.qrCodeName = newLabel.QRCodeValue;
            printDataParam.Add(orderData);
            code = _functionUtility.GenerateCodeIdentity(code);
            data.pqty = dataPrint.PrintQty;
            data.update_time = DateTime.Now;
            _repository.MS_QR_Order.Update(data);
            await _repository.Save();
            try{
                await transaction.CommitAsync();
                return new OperationResult() {IsSuccess = true, Data = printDataParam};
            }
            catch{
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false};
            }
        }

        #endregion

        #region OrderPrintIsBalance --> Balance = true
            private async Task<OperationResult> OrderPrintIsBalance(OrderDataPrint dataPrint, string QRCodeIDDay, string code)
            {
                List<OrderPrintParam> printDataParams = new();
                decimal canPrintQty = 0;
                decimal currentPrintQty = 0;
                using var transaction = await _repository.BeginTransactionAsync();
                foreach(var item in dataPrint.Items)
                {
                    currentPrintQty = Math.Min(item.max_qty_can_print, dataPrint.PrintQty);
                    var compare = await Compare_Qty(item,currentPrintQty);
                    if(compare.IsSuccess)
                    {
                        var data = await GetQROrderRecord(item);
                        if(data == null)
                        {
                            await transaction.RollbackAsync();
                            return new OperationResult() { IsSuccess =false};
                        }
                        short tempSerial =await GetSerial(item);
                        canPrintQty = Math.Min(item.max_qty_can_print, dataPrint.PrintQty);
                        while (canPrintQty> 0)
                        { 
                            var orderQty = Math.Min(Convert.ToInt32(canPrintQty), dataPrint.PackageQty);
                            item.max_qty_can_print -= orderQty;
                            canPrintQty -= orderQty;
                            dataPrint.PrintQty -=(int)orderQty;   
                            var serial = ++tempSerial;
                            OrderPrintParam orderData = await OrderPrintData(data , orderQty, serial, dataPrint);
                            MS_QR_Label newLabel  = await LabelData(data, orderData, dataPrint, $"{QRCodeIDDay}{code}");
                            _repository.MS_QR_Label.Add(newLabel);
                            await _repository.Save();
                            orderData.qrCodeName = newLabel.QRCodeValue;
                            data.pqty += orderData.qty;
                            printDataParams.Add(orderData);
                            code = _functionUtility.GenerateCodeIdentity(code);                          
                        }
                        data.update_time = DateTime.Now;
                        data.updated_by = dataPrint.UserName;
                        _repository.MS_QR_Order.Update(data);
                        await _repository.Save();
                    }
                    else{
                        await transaction.RollbackAsync();
                        return new OperationResult() {IsSuccess = false, Error = "QuantityExceeded", Data = compare.Data};
                    }

                }try{
                      await transaction.CommitAsync();
                    return new OperationResult() { IsSuccess = true, Data = printDataParams };
                }
            catch (System.Exception)
            {
                await transaction.RollbackAsync();
                return new OperationResult() { IsSuccess = false };
            }
            }
        #endregion
         #region OrderPrintIsNonBalance -> Balance = false
        private async Task<OperationResult> OrderPrintIsNonBalance(OrderDataPrint dataPrint, string QRCodeIDDay, string code)
        {
            List<OrderPrintParam> printDataResult = new();
            decimal canPrintQty = 0;
            decimal printQty = dataPrint.PrintQty;
            decimal currentPrintQty = 0;
            using var transaction = await _repository.BeginTransactionAsync();
            foreach (var item in dataPrint.Items)
            {
                currentPrintQty = Math.Min(item.wkshqty, printQty);
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
                        if (orderQty == dataPrint.PackageQty)
                        {
                            var serial = ++tempSerial;
                            OrderPrintParam orderData = await OrderPrintData(data, orderQty, serial, dataPrint);
                            MS_QR_Label newLabel = await LabelData(data, orderData, dataPrint, $"{QRCodeIDDay}{code}");
                            _repository.MS_QR_Label.Add(newLabel);
                            await _repository.Save();
                            orderData.qrCodeName = newLabel.QRCodeValue;
                            data.pqty += orderData.qty;
                            printDataResult.Add(orderData);
                            code = _functionUtility.GenerateCodeIdentity(code);
                        }
                    }
                    data.update_time = DateTime.Now;
                    data.updated_by = dataPrint.UserName;
                    _repository.MS_QR_Order.Update(data);
                    await _repository.Save();
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
        #endregion

        #region Compare_Qty
        private async Task<OperationResult> Compare_Qty(OrderDataItem item, decimal currentPrintQty)
        {
            var listOrder = await _repository.MS_QR_Order
                .FindAll(x => x.purno.Trim() == item.purno.Trim() && x.manno.Trim() == item.manno.Trim() && x.size.Trim() == item.size.Trim())
                .AsNoTracking().ToListAsync();

            decimal sumpqty = listOrder.Sum(x => x.pqty);
            decimal qty = listOrder.Where(x => x.kind.Trim() != "5").OrderByDescending(x => x.mdat).ThenByDescending(x => x.wkshno).FirstOrDefault()?.qty ?? 0;

            return new OperationResult
            {
                IsSuccess = qty >= (sumpqty + currentPrintQty),
                Data = new CompareQtyResult { qty = qty, pqty = sumpqty, printQty = currentPrintQty }
            };
        }
        #endregion
        #region GetQROrderRecord
        private async Task<MS_QR_Order> GetQROrderRecord(OrderDataItem item)
        {
            return await _repository.MS_QR_Order
                .FirstOrDefaultAsync(x => x.purno.Trim() == item.purno.Trim() &&
                    x.manno.Trim() == item.manno.Trim() &&
                    x.size.Trim() == item.size.Trim() &&
                    x.wkshno.Trim() == item.wkshno.Trim() &&
                    x.prtno.Trim() == item.prtno.Trim());
        }
        #endregion
        #region GetSerial
        private async Task<short> GetSerial(OrderDataItem item)
        {
            var mS_QR_Label = await _repository.MS_QR_Label
                .FindAll(
                    x => x.PurNo.Trim() == item.purno.Trim() &&
                    x.ManNo.Trim() == item.manno.Trim() &&
                    x.Size.Trim() == item.size.Trim()
                ).OrderBy(x => x.Serial).LastOrDefaultAsync();
            return mS_QR_Label == null ? Convert.ToInt16(0) : mS_QR_Label.Serial;
        }
        #endregion
        #region OrderPrintData
        private async Task<OrderPrintParam> OrderPrintData(MS_QR_Order data, decimal orderQty, short serial, OrderDataPrint dataPrint)
        {
            OrderPrintParam orderData = new OrderPrintParam()
            {
                purno = data.purno,
                rmodel = data.rmodel,
                manno = data.manno,
                article = data.article,
                size = data.size,
                bitnbr = data.bitnbr,
                custid = data.cusid,
                kind = data.kind,
                qty = orderQty,
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
        private async Task<MS_QR_Label> LabelData(MS_QR_Order data, OrderPrintParam orderData, OrderDataPrint dataPrint, string QRCodeID)
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
                QRCodeID = QRCodeID
            };
            return await Task.FromResult(newLabel);
        }
        #endregion
    }
}