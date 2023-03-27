using API._Repositories;
using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.PackingScan;
using API.DTOs.Transaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_PackingScanServices : I_PackingScanServices
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _config;
        private readonly string _manuf;

        public S_PackingScanServices(IRepositoryAccessor repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
            _manuf = _config.GetSection("Appsettings: FactoryCode").Value;
        }

        public async Task<OperationResult> CheckScanItem(string scanText)
        {
            var item = scanText.Split(',');
            string manNo = item[0];
            string purNo = item[1];
            string size = item[2];
            short serial = short.Parse(item[4]);

            var predicateLabel = PredicateBuilder.New<MS_QR_Label>();
            if (!string.IsNullOrWhiteSpace(manNo))
                predicateLabel.And(x => x.ManNo.Trim() == manNo.Trim());
            if (!string.IsNullOrWhiteSpace(purNo))
                predicateLabel.And(x => x.PurNo.Trim() == purNo.Trim());
            if (!string.IsNullOrWhiteSpace(size))
                predicateLabel.And(x => x.Size.Trim() == size.Trim());
            if (serial > 0)
                predicateLabel.And(x => x.Serial == serial);
            var labelItem = await _repository.MS_QR_Label.FindAll(predicateLabel).FirstOrDefaultAsync();

            if (labelItem != null)
            {
                if (labelItem.Flag == "Y")
                {
                    var predicateSort = PredicateBuilder.New<MS_QR_Sort>(x => x.Manuf.Trim() == labelItem.Manuf.Trim() && x.QRCodeID.Trim() == labelItem.QRCodeID.Trim());
                    var sortItem = await _repository.MS_QR_Sort.FindAll(predicateSort).ToListAsync();
                    if (!sortItem.Any())
                        return new OperationResult(true);
                    return new OperationResult(false, "AlreadyScannedIn2.3");
                }
                return new OperationResult(false, "Cancelled");
            }
            return new OperationResult(false, "LabelNotBePrinted");
        }

        public async Task<List<PackingScanExportDto>> GetDataExport(string transactionNo)
        {
            var data = await GetData(transactionNo);
            data = data.OrderBy(x => x.PurNo).ThenBy(x => x.ManNo).ToList();
            var dataGroup = data.GroupBy(x => new
            {
                x.ManNo,
                x.PurNo,
                x.Size,
            }).Select(x => new PackingScanViewDto
            {
                SDat = x.First().SDat,
                TrNo = x.First().TrNo,
                PrintTime = x.First().PrintTime,
                Shift = x.First().Shift,
                ManNo = x.Key.ManNo,
                PurNo = x.Key.PurNo,
                RModel = x.First().Bitnbr,
                Size = x.Key.Size,
                Qty = x.Sum(y => y.Qty),
                CrUsr = x.First().CrUsr
            }).ToList();

            var result = dataGroup.GroupBy(x => x.ManNo).Select(x => new PackingScanExportDto
            {
                ManNo = x.Key,
                ListItemPerPage = dataGroup.Where(y => y.ManNo == x.Key).ToList(),
                Qty = dataGroup.Where(y => y.ManNo == x.Key).Sum(y => y.Qty)
            }).ToList();

            return result;
        }

        public async Task<PaginationUtility<PackingScanViewDto>> GetDataPage(PaginationParam param, string transactionNo)
        {
            var result = await GetData(transactionNo);
            result = result.OrderBy(x => x.ManNo).ToList();
            return PaginationUtility<PackingScanViewDto>.Create(result, param.PageNumber, param.PageSize);
        }


        public async Task<OperationResult> SavePackingScanList(PackingScanDto data, string username)
        {
            var shift = await _repository.MS_Shift.FirstOrDefaultAsync(x => x.Shift.Trim() == data.Shift.Trim());
            var no = "S" + data.Scan_Date.Replace("/", "");
            var prev_No = await _repository.MS_QR_Sort.FindAll(x => x.TrNo.StartsWith(no)).OrderByDescending(x => x.TrNo).Select(x => x.TrNo).FirstOrDefaultAsync();
            var trNo = no + (prev_No == null ? "001" : (int.Parse(prev_No.Substring(9)) + 1));
            foreach (var scanText in data.ListData)
            {
                var item = scanText.Split(',');
                var manNo = item[0];
                var purNo = item[1];
                var size = item[2];
                var serial = short.Parse(item[4]);
                var labelItem = await _repository.MS_QR_Label.FirstOrDefaultAsync(x => x.PurNo.Trim() == purNo.Trim() && x.ManNo.Trim() == manNo.Trim()
                && x.Size.Trim() == size.Trim() && x.Serial == serial);
                var predicateSort = PredicateBuilder.New<MS_QR_Sort>(x => x.Manuf.Trim() == labelItem.Manuf.Trim() && x.QRCodeID.Trim() == labelItem.QRCodeID.Trim());
                var sortExit = await _repository.MS_QR_Sort.FindAll(predicateSort).ToListAsync();
                if (!sortExit.Any())
                {
                    var sortData = new MS_QR_Sort
                    {
                        Manuf = labelItem.Manuf,
                        SDat = DateTime.Parse(data.Scan_Date),
                        TrNo = trNo,
                        Shift = shift.Shift,
                        Grade = labelItem.Grade,
                        QRCodeID = labelItem.QRCodeID,
                        ManNo = labelItem.ManNo,
                        PurNo = labelItem.PurNo,
                        Serial = labelItem.Serial,
                        Size = labelItem.Size,
                        Qty = labelItem.Qty,
                        CrUsr = username,
                        CrDay = DateTime.Now,
                        EndCod = "N"
                    };
                }
                else
                    return new OperationResult(false, "AlreadyScannedIn2.3");
            }
            if (await _repository.Save())
                return new OperationResult(true, "", trNo);
            else
                return new OperationResult(false);
        }

        private async Task<List<PackingScanViewDto>> GetData(string transactionNo)
        {
            string currentDate = DateTime.Now.ToString("yyyy/MM/ddd HH:mm:ss");
            var dataSort = _repository.MS_QR_Sort.FindAll(x => x.Manuf == _manuf);
            var dataLabel = _repository.MS_QR_Label.FindAll(x => x.Manuf == _manuf);
            var dataOrder = _repository.MS_QR_Order.FindAll(x => x.manuf == _manuf);
            if (!string.IsNullOrWhiteSpace(transactionNo.Trim()))
                dataSort = dataSort.Where(x => x.TrNo.Contains(transactionNo));
            return await dataSort.Join(
                dataOrder,
                x => new { x.ManNo, x.PurNo, x.Size, x.Manuf },
                y => new { ManNo = y.manno, PurNo = y.purno, Size = y.size, Manuf = y.manuf },
                (x, y) => new { sort = x, order = y }).Join(
                    dataLabel,
                    x => new { x.sort.ManNo, x.sort.PurNo, x.sort.Size, x.sort.Serial, x.sort.Manuf },
                    y => new { y.ManNo, y.PurNo, y.Size, y.Serial, y.Manuf },
                    (x, y) => new { x.order, x.sort, label = y })
                    .Select(x => new PackingScanViewDto
                    {
                        Article = x.order.article,
                        Bitnbr = x.order.bitnbr,
                        CrDay = x.sort.CrDay,
                        CrUsr = x.sort.CrUsr,
                        CyNo = x.label.CyNo,
                        EndCod = x.sort.EndCod,
                        ManNo = x.order.manno,
                        PurNo = x.order.purno,
                        Manuf = x.sort.Manuf,
                        Qty = x.label.Qty,
                        RModel = x.order.rmodel,
                        SDat = x.sort.SDat.ToString("yyyy/MM/dd"),
                        Serial = x.label.Serial,
                        Shift = x.sort.Shift,
                        TrNo = x.sort.TrNo,
                        Grade = x.sort.Grade,
                        QRCodeID = x.sort.QRCodeID,
                        Size = x.order.size,
                        style = x.order.style,
                        Seq = x.label.Seq,
                        PrintTime = currentDate
                    }).Distinct().ToListAsync();
        }
    }
}