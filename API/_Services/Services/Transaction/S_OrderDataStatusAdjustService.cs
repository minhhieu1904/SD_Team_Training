using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.transaction;
using API.Dtos.Transaction.OrderDataStatusAdjust;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_OrderDataStatusAdjustService : I_OrderDataStatusAdjustService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _config;
        private readonly string _manuf;

        public S_OrderDataStatusAdjustService(IRepositoryAccessor repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
            _manuf = _config.GetSection("Appsettings:FactoryCode").Value;
        }

        public async Task<OrderDataStatusAdjustResponse> GetDataPagination(PaginationParam param, OrderDataStatusAdjustParam filter)
        {
            var pred = PredicateBuilder.New<MS_QR_Order>(x => x.manuf == _manuf);
            if (!string.IsNullOrWhiteSpace(filter.PlanningNo))
                pred.And(x => x.manno.Trim() == filter.PlanningNo.Trim() || x.manno.Trim().Contains(filter.PlanningNo));
            if (!string.IsNullOrWhiteSpace(filter.Purno))
                pred.And(x => x.purno.Trim() == filter.Purno.Trim());
            if (!string.IsNullOrWhiteSpace(filter.Tolcls))
                pred.And(x => x.tolcls.Trim() == filter.Tolcls.Trim());

            var orders = await _repository.MS_QR_Order.FindAll(pred)
            .Select(x => new OrderDataStatusAdjustDto()
            {
                Brand = x.brandname,
                MoldNumber = x.rmodel,
                Tolcls = x.tolcls,
                MaterialCodeBottom = x.bitnbr,
                Size = x.size,
                CodeOfCustomer = x.cusid,
                PlanningNo = x.manno,
                PurchaseNo = x.purno,
                Status = x.endcod,
                WkshNo = x.wkshno,
                NameOfCustomer = x.cusna,
                TypeOfOrder = x.kind,
                ConfirmedDeliveryDate = x.eta,
                Article = x.article,
                OrderQuantity = x.qty,
                WkshQty = x.wkshqty,
                IncreaseOrderQuantity = x.addqty,
                ReduceOrderQuantity = x.lessqty,
                AvailableQuantityForPrint = $"{x.qty} - {x.addqty} - {x.lessqty} - {x.pqty}",
                PrintedQuantity = x.pqty,
                CancelPritingQuantity = x.cqty,
                MaterialCodeShoes = x.ritnbr,
                ModelName = x.style,
                ToolingSize = x.tsize,
                Remark = x.remark,
                TimeOfUpdating = x.update_time,
                UpdatedBy = x.updated_by,
                prtno = x.prtno
            }).OrderBy(x => x.Size).ToListAsync();
            return new OrderDataStatusAdjustResponse()
            {
                AllData = orders,
                Pagination = PaginationUtility<OrderDataStatusAdjustDto>.Create(orders, param.PageNumber, param.PageSize)
            };
        }

        public async Task<OperationResult> Update(OrderDataStatusAdjustUpdate model)
        {
            if (!model.OrderDataStatusAdjusts.Any())
                return new OperationResult(false, "No data");

            var orderData = new List<MS_QR_Order>();
            foreach (var item in model.OrderDataStatusAdjusts)
            {
                var qrOderItem = await _repository.MS_QR_Order.FirstOrDefaultAsync(x => x.manuf == _manuf
                                                            && x.manno.Trim() == item.PlanningNo.Trim()
                                                            && x.purno.Trim() == item.PurchaseNo.Trim()
                                                            && x.size.Trim() == item.Size.Trim()
                                                            && x.wkshno.Trim() == item.WkshNo.Trim()
                                                            && x.prtno.Trim() == item.prtno.Trim());
                if (qrOderItem == null)
                    return new OperationResult(false, $"QR Order with manno: ${item.PlanningNo}, purno: {item.PurchaseNo}, Size: {item.Size}, WkshNo: {item.WkshNo}, prtno: {item.prtno} not exist");
                qrOderItem.endcod = model.EndCod;
                orderData.Add(qrOderItem);
            }

            if (!orderData.Any())
                return new OperationResult(false, "No data updated");
            _repository.MS_QR_Order.UpdateMultiple(orderData);
            await _repository.Save();

            return new OperationResult(true);
        }
    }
}