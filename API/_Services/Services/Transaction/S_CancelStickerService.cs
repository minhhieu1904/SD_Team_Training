using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.CancelSticker;
using API.Helpers.Params.Transaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_CancelStickerService : I_CancelStickerService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        protected readonly string _manuf;

        public S_CancelStickerService(IRepositoryAccessor repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsetting:FactoryCode").Value;
        }

        public async Task<OperationResult> CancelStickerAction(List<string> datas, string userName)
        {
            foreach (var qrCodeValue in datas)
            {
                var check = await CheckRecordScan(qrCodeValue);
                if (!check.IsSuccess)
                    return new OperationResult { IsSuccess = false, Error = check.Error, Data = check.Data };
            }

            return await UpdateDataCancel(datas, userName);
        }

        private async Task<OperationResult> UpdateDataCancel(List<string> datas, string userName)
        {
            var now = DateTime.Now;
            foreach (var qrCodeValue in datas)
            {
                var targets = qrCodeValue.Split(',');
                var manNo = targets[0].Trim();
                var purNo = targets[1].Trim();
                var size = targets[2].Trim();
                var qty = decimal.Parse(targets[3].Trim());
                var serial = short.Parse(targets[4].Trim());
                var wkshno = targets[5].Trim();
                var prtno = targets[6].Trim();

                var order = await _repository.MS_QR_Order
                    .FirstOrDefaultAsync(
                        x => x.manuf == _manuf &&
                        x.purno.Trim() == purNo.Trim() &&
                        x.manno.Trim() == manNo.Trim() &&
                        x.size.Trim() == size.Trim() &&
                        x.wkshno.Trim() == wkshno &&
                        x.prtno.Trim() == prtno);

                if (order != null)
                {
                    order.pqty -= qty;
                    order.cqty += qty;
                    _repository.MS_QR_Order.Update(order);
                }

                var label = await _repository.MS_QR_Label
                    .FirstOrDefaultAsync(
                        x => x.Manuf == _manuf &&
                        x.PurNo.Trim() == purNo.Trim() &&
                        x.ManNo.Trim() == manNo.Trim() &&
                        x.Size.Trim() == size.Trim() &&
                        x.wkshno.Trim() == wkshno &&
                        x.prtno.Trim() == prtno &&
                        x.Serial == serial);
                if (label != null)
                {
                    label.Flag = "C";
                    label.CanUsr = userName;
                    label.CanDay = now;
                    _repository.MS_QR_Label.Update(label);
                }
            }
            return new OperationResult { IsSuccess = await _repository.Save() };
        }

        public async Task<OperationResult> CancelStickerScan(List<string> datas, string userName)
        {
            return await UpdateDataCancel(datas, userName);
        }

        public async Task<OperationResult> CheckRecordScan(string qrCodeValue)
        {
            var label = await _repository.MS_QR_Label.FirstOrDefaultAsync(x => x.Manuf == _manuf && x.QRCodeValue.Trim() == qrCodeValue.Trim());
            if (label?.Flag.Trim() == "C" || label == null)
                return new OperationResult { IsSuccess = false, Error = "LabelFlagC", Data = label?.QRCodeID };
            var storage = await _repository.MS_QR_Storage.FirstOrDefaultAsync(x => x.QRCodeID.Trim() == label.QRCodeID.Trim());
            return new OperationResult { IsSuccess = storage == null, Error = "NotCancel", Data = storage?.QRCodeID };

        }

        public async Task<PaginationUtility<CancelStickerViewModel>> GetDataPagination(PaginationParam pagination, CancelStickerParam param, bool isPaging = true)
        {
            var labelPredicate = PredicateBuilder.New<MS_QR_Label>(x => x.Flag.Trim() != "C");
            if (!string.IsNullOrWhiteSpace(param.ManNo))
                labelPredicate.And(x => x.ManNo == param.ManNo);
            if (!string.IsNullOrWhiteSpace(param.PurNo))
                labelPredicate.And(x => x.PurNo == param.PurNo);
            if (!string.IsNullOrWhiteSpace(param.Size))
                labelPredicate.And(x => x.Size == param.Size);
            if (param.Serial.HasValue && param.Serial > 0)
                labelPredicate.And(x => x.Serial == param.Serial);

            var data = _repository.MS_QR_Label.FindAll(labelPredicate).AsNoTracking()
            .GroupJoin(_repository.MS_QR_Sort.FindAll().AsNoTracking(),
            x => x.QRCodeID,
            y => y.QRCodeID,
            (x, y) => new { label = x, sort = y }).SelectMany(x => x.sort.DefaultIfEmpty(), (x, y) => new { label = x.label, sort = y })
            .Select(x => new CancelStickerViewModel
            {
                IsSortScan = x.sort != null && !string.IsNullOrWhiteSpace(x.sort.QRCodeID) ? "Y" : "N",
                ManNo = x.label.ManNo,
                PurNo = x.label.PurNo,
                Size = x.label.Size,
                Serial = x.label.Serial,
                Grade = x.label.Grade,
                wkshno = x.label.wkshno,
                Qty = x.label.Qty,
                Prt_Cnt = x.label.Prt_Cnt,
                empno = x.label.empno,
                CrUsr = x.label.CrUsr,
                PrtDay = x.label.PrtDay,
                QRCodeID = x.label.QRCodeID,
                QRCodeValue = x.label.QRCodeValue
            });
            return await PaginationUtility<CancelStickerViewModel>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }
    }
}