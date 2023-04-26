
using API._Repositories;
using API._Services.Interfaces.Transaction.PackingScan;
using API.DTOs.Transaction.PackingScan;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Transaction.PackingScan
{
    public class PackingScanService : IPackingScanService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        protected readonly string _manuf;
        public PackingScanService(IRepositoryAccessor repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value;
        }
        public async Task<List<KeyValuePair<string, string>>> getListShift()
        {
            return await _repository.MS_Shift.FindAll().Select(x => new KeyValuePair<string, string>(x.Shift, x.ShiftName)).Distinct().ToListAsync();
        }
        public async Task<OperationResult> CheckScanItem(string scanText)
        {
            var item = scanText.Split(',');
            string ManNo  = item[0];
            string PurNo = item[1];
            string Size = item[2];
            short Serial = short.Parse(item[4]);

            var predicateLabel =  PredicateBuilder.New<MS_QR_Label>(true);
            if(!string.IsNullOrWhiteSpace(ManNo))
                predicateLabel.And(x=>x.ManNo.Trim() == ManNo.Trim());
            if(!string.IsNullOrWhiteSpace(PurNo))
                predicateLabel.And(x=>x.PurNo.Trim() == PurNo.Trim());
            if(!string.IsNullOrWhiteSpace(Size))
                predicateLabel.And(x=>x.Size.Trim() == Size.Trim());
            if(Serial > 0)
                predicateLabel.And(x=>x.Serial == Serial);
            var labelItem = await _repository.MS_QR_Label.FindAll(predicateLabel).FirstOrDefaultAsync();
            if(labelItem != null)
            {
                if(labelItem.Flag == "Y")
                {
                    var predicateSort = PredicateBuilder.New<MS_QR_Sort>(x => x.Manuf.Trim() == labelItem.Manuf.Trim() && x.QRCodeID.Trim()  == labelItem.QRCodeID.Trim());
                    var sortItem = await _repository.MS_QR_Sort.FindAll(predicateSort).ToListAsync(); 
                    if(!sortItem.Any())
                        return new OperationResult(true);
                    return new OperationResult(false, "AlreadyScannedIn2.3");
                }
                return new OperationResult(false, "Cancelled");

            }
            return new OperationResult(false, "LabelNotBePrinted");
        }

        public async Task<List<PackingScanExportDTO>> GetDataExport(string TransactionNo)
        {
            var data = await GetData(TransactionNo);
            data = data.OrderBy(x => x.purNo).ThenBy(x => x.manNo).ToList();
            var dataGrouped = data.GroupBy(x => new { x.manNo, x.purNo, x.size }).Select(x => new PackingScanViewDTO
            {
                SDat = x.First().SDat,
                TrNo = x.First().TrNo,
                PrintTime = x.First().PrintTime,
                Shift = x.First().Shift,
                manNo = x.Key.manNo,
                purNo = x.Key.purNo,
                RModel = x.First().RModel,
                bitnbr = x.First().bitnbr,
                size = x.Key.size,
                label_Qty = x.Sum(y => y.label_Qty),
                crUsr = x.First().crUsr
            }).ToList();       
              var result = dataGrouped.GroupBy(x => x.manNo).Select(x => new PackingScanExportDTO
            {
                manNo = x.Key,
                ListItemPerPage = dataGrouped.Where(y => y.manNo == x.Key).ToList(),
                Qty = dataGrouped.Where(y => y.manNo == x.Key).Sum(y => y.label_Qty)
            }).ToList();

            return result; 
        }
        public async Task<PaginationUtility<PackingScanViewDTO>> GetList(PaginationParam paginationParam, string TransactionNo)
        {
            var result = await GetData(TransactionNo);
            result = result.OrderBy(x=>x.manNo).ToList();
            return PaginationUtility<PackingScanViewDTO>.Create(result, paginationParam.PageNumber, paginationParam.PageSize);
        }

        public async Task<OperationResult> SavePackingScanList(PackingScanDTOParam data, string userName)
        {
            var currentDate = DateTime.Now;
            var shift = await _repository.MS_Shift.FirstOrDefaultAsync(x => x.Shift.Trim() == data.shift.Trim());
            var no = "S" + currentDate.ToString("yyyyMMdd");
            var prev_no = await _repository.MS_QR_Sort.FindAll(x => x.TrNo.StartsWith(no)).OrderByDescending(x => x.TrNo).Select(x => x.TrNo).FirstOrDefaultAsync();
            var TrNo = no + (prev_no == null ? "001" : (int.Parse(prev_no.Substring(9)) + 1).ToString("000"));

            foreach (var scanText in data.ListData)
            {
                var item = scanText.Split(',');
                var ManNo = item[0];
                var PurNo = item[1];
                var Size = item[2];
                var Serial = short.Parse(item[4]);
                var labelItem = await _repository.MS_QR_Label.FirstOrDefaultAsync(x => x.PurNo.Trim() == PurNo.Trim() && x.ManNo.Trim() == ManNo.Trim() && x.Size.Trim() == Size.Trim() && x.Serial == Serial);
                var sortData = new MS_QR_Sort
                {
                    Manuf = labelItem.Manuf,
                    SDat = currentDate,
                    TrNo = TrNo,
                    Shift = shift.Shift,
                    Grade = labelItem.Grade,
                    QRCodeID = labelItem.QRCodeID,
                    ManNo = labelItem.ManNo,
                    PurNo = labelItem.PurNo,
                    Serial = labelItem.Serial,
                    Size = labelItem.Size,
                    Qty = labelItem.Qty,
                    CrUsr = userName,
                    CrDay = currentDate,
                    EndCod = "N"
                };
                _repository.MS_QR_Sort.Add(sortData);
            }
            if (await _repository.Save())
                 return new OperationResult(true, "", TrNo);
             else
                 return new OperationResult(false);
        }
           private async Task<List<PackingScanViewDTO>> GetData(string TransactionNo)
        {
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var dataSort = _repository.MS_QR_Sort.FindAll(x => x.Manuf == _manuf);
            var dataLabel = _repository.MS_QR_Label.FindAll(x => x.Manuf == _manuf);
            var dataOrder = _repository.MS_QR_Order.FindAll(x => x.manuf == _manuf);
            if (!string.IsNullOrEmpty(TransactionNo))
                dataSort = dataSort.Where(x => x.TrNo.Contains(TransactionNo));

            return await dataSort.Join(
                dataOrder,
                x => new { x.ManNo, x.PurNo, x.Size, x.Manuf },
                y => new { ManNo = y.manno, PurNo = y.purno, Size = y.size, Manuf = y.manuf },
                (x, y) => new { sort = x, order = y })
                    .Join(dataLabel,
                    x => new { x.sort.ManNo, x.sort.PurNo, x.sort.Size, x.sort.Serial, x.sort.Manuf },
                    y => new { y.ManNo, y.PurNo, y.Size, y.Serial, y.Manuf },
                    (x, y) => new { x.order, x.sort, label = y })
                        .Select(x => new PackingScanViewDTO
                        {
                            article = x.order.article,
                            bitnbr = x.order.bitnbr,
                            crDay = x.sort.CrDay,
                            crUsr = x.sort.CrUsr,
                            cyNo = x.label.CyNo,
                            endCod = x.sort.EndCod,
                            manNo = x.order.manno,
                            purNo = x.order.purno,
                            manuf = x.sort.Manuf,
                            label_Qty = x.label.Qty,
                            RModel = x.order.rmodel,
                            SDat = x.sort.SDat.ToString("yyyy/MM/dd"),
                            serial = x.label.Serial,
                            Shift = x.sort.Shift,
                            TrNo = x.sort.TrNo,
                            Grade = x.sort.Grade,
                            QRCodeID = x.sort.QRCodeID,
                            size = x.order.size,
                            style = x.order.style,
                            seq = x.label.Seq,
                            PrintTime = currentDate
                        }).Distinct().ToListAsync();
        }
    }
}