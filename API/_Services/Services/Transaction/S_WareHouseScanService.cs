
using API._Repositories;
using API._Services.Interfaces.Transaction;
using API.DTOs.Transaction.WarehouseScan;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_WareHouseScanService : I_WareHouseScanService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;

        public S_WareHouseScanService(IRepositoryAccessor repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration["Appsettings:FactoryCode"];
        }
        public async Task<OperationResult> CheckScanCode(string target)
        {
            var item = target.Split(',');
            string ManNo = item[0];
            string PurNo = item[1];
            string Size = item[2];
            short Serial = short.Parse(item[4]);
            var predicate = PredicateBuilder.New<MS_QR_Label>(true);
            if (!string.IsNullOrWhiteSpace(ManNo))
                predicate.And(x => x.ManNo.Trim() == ManNo.Trim());
            if (!string.IsNullOrWhiteSpace(PurNo))
                predicate.And(x => x.PurNo.Trim() == PurNo.Trim());
            if (!string.IsNullOrWhiteSpace(Size))
                predicate.And(x => x.Size.Trim() == Size.Trim());
            if (Serial > 0)
                predicate.And(x => x.Serial == Serial);
            var labelItem = await _repository.MS_QR_Label.FindAll(predicate).FirstOrDefaultAsync();
            if (labelItem != null)
            {
                if (labelItem.Flag == "Y")
                {
                    var predicateSort = PredicateBuilder.New<MS_QR_Sort>(x => x.Manuf.Trim() == labelItem.Manuf.Trim() && x.QRCodeID.Trim() == labelItem.QRCodeID.Trim());
                    var sortItem = await _repository.MS_QR_Sort.FindAll(predicateSort).ToListAsync();
                    if (sortItem.Any())
                    {
                        var predicateStorage = PredicateBuilder.New<MS_QR_Storage>(x => x.manuf.Trim() == labelItem.Manuf.Trim() && x.QRCodeID.Trim() == labelItem.QRCodeID.Trim());
                        var storageItem = await _repository.MS_QR_Storage.FindAll(predicateStorage).ToListAsync();
                        if (!storageItem.Any())
                            return new OperationResult(true);
                        return new OperationResult(false, "AlreadyScannedIn2_4");
                    }
                    return new OperationResult(false, "NotReadyScannedIn2_3");
                }
                return new OperationResult(false, "LabelHasBeenCancelled");
            }
            return new OperationResult(false, "LabelNotBePrinted");
        }

        public async Task<PaginationUtility<MS_QR_StorageDto>> GetListQRStorage(PaginationParam pagination, string trNo)
        {
            var data = await (_repository.MS_QR_Storage.FindAll(x => x.trno.Contains(trNo))
            .Join(_repository.MS_QR_Order.FindAll(),
            s => new { s.purno, s.manno, s.size, s.wkshno, s.prtno },
            o => new { purno = o.purno.Trim(), manno = o.manno.Trim(), size = o.size.Trim(), o.wkshno, o.prtno },
            (s, o) => new { storage = s, order = o })

            .Join(_repository.MS_QR_Label.FindAll(),
            x => new { x.storage.purno, x.storage.manno, x.storage.size, x.storage.wkshno, x.storage.prtno },
            l => new { purno = l.PurNo.Trim(), manno = l.ManNo.Trim(), size = l.Size.Trim(), l.wkshno, l.prtno },
            (x, l) => new { x.storage, x.order, label = l })
            .Select(x => new MS_QR_StorageDto
            {
                TrNo = x.storage.trno,
                SDat = x.storage.Sdat,
                StoreH = x.storage.storeh,
                ParNo = x.storage.parno,
                ManNo = x.storage.manno,
                PurNo = x.storage.purno,
                Serial = x.storage.serial,
                Size = x.storage.size,
                Qty = x.storage.qty,
                Style = x.order.size.Trim(),
                BitNbr = x.order.bitnbr.Trim(),
                Article = x.order.article,
                RModel = x.order.rmodel.Trim(),
                CyNo = x.label.CyNo,
                Seq = x.label.Seq,
            })).ToListAsync();
            return PaginationUtility<MS_QR_StorageDto>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<List<WarehouseScanExportDto>> GetQRStorageExport(string trNo)
        {
            var data = await _repository.MS_QR_Storage.FindAll(x => x.trno.Contains(trNo))
                .GroupJoin(_repository.MS_QR_Order.FindAll(),
                x => new { x.purno, x.manno, x.size, x.wkshno, x.prtno },
                y => new { y.purno, y.manno, y.size, y.wkshno, y.prtno },
                (x, y) => new { storage = x, order = y })
                .SelectMany(
                    x => x.order.DefaultIfEmpty(),
                    (a, b) => new { a.storage, order = b }
                ).GroupBy(x => x.storage.manno)
                .Select(g => new WarehouseScanExportDto
                {
                    Key = g.Key,
                    ListWarehouseScan = g.GroupBy(x => new { x.storage.manno, x.storage.purno, x.storage.size })
                                    .Select(x => new MS_QR_StorageDto
                                    {
                                        TrNo = x.Max(x => x.storage.trno),
                                        SDat = x.Max(x => x.storage.Sdat),
                                        StoreH = x.Max(x => x.storage.storeh),
                                        ParNo = x.Max(x => x.storage.parno),
                                        ManNo = x.Key.manno,
                                        PurNo = x.Key.purno,
                                        Size = x.Key.size,
                                        Qty = x.Max(x => x.storage.qty),
                                        BitNbr = x.Max(x => x.order.bitnbr),
                                        RModel = x.Max(x => x.order.rmodel),
                                    }).ToList(),
                    Qty = (int)Math.Floor(g.Sum(x => x.storage.qty))
                }).ToListAsync();
            return data;
        }

        public async Task<OperationResult> SaveQRStorage(WarehouseScanDto data)
        {
            var no = "W" + data.Scan_Date.Replace("/", "");
            var prevNo = await _repository.MS_QR_Storage.FindAll(x => x.trno.Substring(0, 9) == no).OrderByDescending(x => x.crday).Select(x => x.trno).FirstOrDefaultAsync();
            var trNo = no + (prevNo == null ? "001" : (int.Parse(prevNo.Substring(9)) + 1).ToString("000"));
            List<MS_QR_Storage> storageDataList = new List<MS_QR_Storage>();
            foreach (var item in data.Label)
            {
                var labelItem = item.Split(',');
                var ManNo = labelItem[0];
                var PurNo = labelItem[1];
                var Size = labelItem[2];
                var Serial = short.Parse(labelItem[4].Trim());
                var predicateLabel = PredicateBuilder.New<MS_QR_Label>(true);
                if (!string.IsNullOrWhiteSpace(ManNo))
                    predicateLabel.And(x => x.ManNo.Trim() == ManNo.Trim());
                if (!string.IsNullOrWhiteSpace(PurNo))
                    predicateLabel.And(x => x.PurNo.Trim() == PurNo.Trim());
                if (!string.IsNullOrWhiteSpace(Size))
                    predicateLabel.And(x => x.Size.Trim() == Size.Trim());
                if (Serial > 0)
                    predicateLabel.And(x => x.Serial == Serial);
                var dataLabel = await _repository.MS_QR_Label.FindAll(predicateLabel).FirstOrDefaultAsync();
                if (dataLabel != null)
                {
                    var predicateSort = PredicateBuilder.New<MS_QR_Sort>(x => x.Manuf.Trim() == dataLabel.Manuf.Trim() && x.QRCodeID.Trim() == dataLabel.QRCodeID.Trim());
                    var sortItem = await _repository.MS_QR_Sort.FindAll(predicateSort).ToListAsync();
                    if (sortItem.Any())
                    {
                        var predicateStorage = PredicateBuilder.New<MS_QR_Storage>(x => x.manuf.Trim() == dataLabel.Manuf.Trim() && x.QRCodeID.Trim() == dataLabel.QRCodeID.Trim());
                        var storageItem = await _repository.MS_QR_Storage.FindAll(predicateStorage).ToListAsync();
                        if (!storageItem.Any())
                        {
                            var dataSort = await _repository.MS_QR_Sort.FirstOrDefaultAsync(x => x.PurNo.Trim() == PurNo.Trim() && x.ManNo.Trim() == ManNo.Trim() && x.Size.Trim() == Size.Trim() && x.Serial == Serial);
                            var dataOrder = await _repository.MS_QR_Order.FirstOrDefaultAsync(x => x.purno.Trim() == PurNo.Trim() && x.manno.Trim() == ManNo.Trim() && x.size.Trim() == Size.Trim() && x.wkshno == dataLabel.wkshno && x.prtno == dataLabel.prtno);
                            var storageData = new MS_QR_Storage
                            {
                                manuf = dataLabel.Manuf,
                                Sdat = DateTime.Parse(data.Scan_Date),
                                iodat = DateTime.Now,
                                trno = trNo,
                                storeh = data.Location,
                                parno = data.Department,
                                manno = dataLabel.ManNo.TrimEnd(),
                                Grade = dataLabel.Grade,
                                QRCodeID = dataLabel.QRCodeID,
                                purno = dataLabel.PurNo,
                                serial = dataLabel.Serial,
                                size = dataLabel.Size,
                                qty = dataLabel.Qty,
                                crusr = dataLabel.CrUsr,
                                crday = dataLabel.CrDay,
                                biz_flag = "N",
                                wkshno = dataOrder.wkshno,
                                prtno = dataOrder.prtno,
                                wkshqty = dataOrder.wkshqty,
                                mdat = dataOrder.mdat,
                                uscod = dataOrder.uscod,
                                empno = dataLabel.empno,
                                bitnbr = dataOrder.bitnbr,
                                ritnbr = dataOrder.ritnbr,
                                IsPicking = "N",
                                IsStorageOut = "N"
                            };
                            dataSort.EndCod = "Y";
                            storageDataList.Add(storageData);
                        }
                        else
                            return new OperationResult(false, "AlreadyScannedIn2_4");
                    }
                    else
                        return new OperationResult(false, "NotReadyScannedIn2_3");
                }
                else
                    return new OperationResult(false, "LabelNotBePrinted");
            }
            try
            {
                var recent_prevNo = await _repository.MS_QR_Storage.FindAll(x => x.trno.Substring(0, 9) == no).OrderByDescending(x => x.crday).Select(x => x.trno).FirstOrDefaultAsync();
                if (prevNo != recent_prevNo)
                {
                    trNo = no + (recent_prevNo == null ? "001" : (int.Parse(recent_prevNo.Substring(9) + 1).ToString("000")));
                    storageDataList.ForEach(val => val.trno = trNo);
                }
                _repository.MS_QR_Storage.AddMultiple(storageDataList);
                await _repository.Save();
                return new OperationResult(true, "", trNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new OperationResult(false);
            }
        }
    }
}