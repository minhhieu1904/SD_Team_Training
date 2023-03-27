using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.transaction;
using API.Dtos.Transaction.ReprintSticker;
using API.Helpers.Params.Transaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_ReprintStickerService : I_ReprintStickerService
    {
        private readonly IRepositoryAccessor _repository;

        public S_ReprintStickerService(IRepositoryAccessor repository)
        {
            _repository = repository;
        }

        public async Task<PaginationUtility<ReprintStickerModel>> GetData(PaginationParam pagination, ReprintStickerParam filterParam)
        {
            var predMS_QR_Order = PredicateBuilder.New<MS_QR_Order>(true);
            var predMS_QR_Label = PredicateBuilder.New<MS_QR_Label>(x => x.Flag != "C");
            if (!string.IsNullOrWhiteSpace(filterParam.fromDate) && !string.IsNullOrWhiteSpace(filterParam.toDate))
            {
                var startTime = Convert.ToDateTime(filterParam.fromDate + " 00:00:00.000");
                var endTime = Convert.ToDateTime(filterParam.toDate + " 23:59:59.997");
                predMS_QR_Order.And(x => x.mdat >= startTime && x.mdat <= endTime);
            }

            if (!string.IsNullOrWhiteSpace(filterParam.prtno))
            {
                predMS_QR_Order.And(x => x.prtno.Trim() == filterParam.prtno.Trim());
            }

            if (filterParam.serial != 0)
            {
                predMS_QR_Label.And(x => x.Serial == filterParam.serial);
            }

            if (!string.IsNullOrWhiteSpace(filterParam.ManNo))
            {
                predMS_QR_Label.And(x => x.ManNo.Trim() == filterParam.ManNo.Trim());
            }

            if (!string.IsNullOrWhiteSpace(filterParam.PurNo))
            {
                predMS_QR_Label.And(x => x.PurNo.Trim() == filterParam.PurNo.Trim());
            }

            if (!string.IsNullOrWhiteSpace(filterParam.Size))
            {
                predMS_QR_Label.And(x => x.Size.Trim() == filterParam.Size.Trim());
            }

            var orders = _repository.MS_QR_Order.FindAll(predMS_QR_Order);
            var labels = _repository.MS_QR_Label.FindAll(predMS_QR_Label);

            var data = labels.Join(
                orders,
                x => new { manNo = x.ManNo, purNo = x.PurNo, size = x.Size, x.wkshno },
                y => new { manNo = y.manno, purNo = y.purno, y.size, y.wkshno },
                (x, y) => new { labels = x, orders = y }
            ).Select(x => new ReprintStickerModel
            {
                Manuf = x.labels.Manuf,
                QRCodeID = x.labels.QRCodeID,
                mdat = x.orders.mdat,
                Grade = x.labels.Grade,
                prtno = x.orders.prtno,
                wkshno = x.labels.wkshno,
                ManNo = x.labels.ManNo,
                PurNo = x.labels.PurNo,
                Size = x.labels.Size,
                Serial = x.labels.Serial,
                Qty = x.labels.Qty,
                Prt_Cnt = x.labels.Prt_Cnt,
                QRCodeValue = x.labels.QRCodeValue,
                brandname = x.orders.brandname,
                cusid = x.orders.cusid,
                cusna = x.orders.cusna,
                bitnbr = x.orders.bitnbr,
                ritnbr = x.orders.ritnbr,
                rmodel = x.orders.rmodel,
                article = x.orders.article,
                empno = x.labels.empno,
                CrUsr = x.labels.CrUsr,
                PrtDay = x.labels.PrtDay,
                kind = x.orders.kind,
                remark = x.orders.remark
            }).Distinct();
            return await PaginationUtility<ReprintStickerModel>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, true);
        }

        public async Task<List<ReprintStickerModel>> GetDataByScan(List<ReprintStickerModel> listModel)
        {
            foreach (var item in listModel)
            {
                var label = await _repository.MS_QR_Label.FirstOrDefaultAsync(x => x.ManNo.Trim() == item.ManNo.Trim() &&
                                                                            x.PurNo.Trim() == item.PurNo.Trim() &&
                                                                            x.Size.Trim() == item.Size.Trim() &&
                                                                            x.Qty == item.Qty &&
                                                                            x.Serial == item.Serial &&
                                                                            x.wkshno == item.wkshno &&
                                                                            x.prtno.Trim() == item.prtno.Trim() &&
                                                                            x.empno.Trim() == item.empno.Trim() &&
                                                                            x.Grade.Trim() == item.Grade.Trim() &&
                                                                            x.QRCodeValue.Trim() == item.QRCodeValue.Trim());
                if (label != null)
                {
                    item.Manuf = label.Manuf;
                    item.QRCodeID = label.QRCodeID;
                    item.PrtDay = label.PrtDay;
                    var order = await _repository.MS_QR_Order.FirstOrDefaultAsync(x => x.manno.Trim() == label.ManNo.Trim() &&
                                                                                x.purno.Trim() == label.PurNo.Trim() &&
                                                                                x.size.Trim() == label.Size.Trim());
                    if (order != null)
                    {
                        item.rmodel = order.rmodel;
                        item.kind = order.kind;
                        item.cusid = order.cusid;
                        item.article = order.article;
                        item.bitnbr = order.bitnbr;
                        item.remark = order.remark;
                    }
                }
            }
            return listModel;
        }

        public async Task<bool> UpdateData(List<ReprintStickerModel> data)
        {
            foreach (var item in data)
            {
                var model = await _repository.MS_QR_Label.FirstOrDefaultAsync(x => x.Manuf.Trim() == item.Manuf.Trim() &&
                                                                            x.QRCodeID.Trim() == item.QRCodeID.Trim());
                if (model != null)
                {
                    model.Prt_Cnt = Convert.ToInt16(model.Prt_Cnt + 1);
                }
            }
            return await _repository.Save();
        }
    }
}