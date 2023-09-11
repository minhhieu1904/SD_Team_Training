
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.PackingScan;
using API.Models;
using Microsoft.EntityFrameworkCore;
using SDCores;
namespace API._Services.Services
{
    public class PackingScanService : IPackingScanService
    {
        private readonly IRepositoryAccessor _repository;

        private readonly IConfiguration _configuration;
        protected readonly string _manuf;
        public PackingScanService(IRepositoryAccessor repositoryAccessor, IConfiguration configuration)
        {
            _repository = repositoryAccessor;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value;
        }

        public async Task<OperationResult> SavePackingScanList(PackingScanDTO data, string userName)
        {
           var currentDate = DateTime.Now;
            var shift = await _repository.MS_Shift.FindSingle(x => x.Shift.Trim() == data.Shift.Trim());
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
                var labelItem = await _repository.MS_QR_Label.FindSingle(x => x.PurNo.Trim() == PurNo.Trim() && x.ManNo.Trim() == ManNo.Trim() && x.Size.Trim() == Size.Trim() && x.Serial == Serial);
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

        public async Task<OperationResult> CheckScanItem(string scanText)
        {
            var item = scanText.Split(',');
            var ManNo = item[0];
            var PurNo = item[1];
            var Size = item[2];
            var Serial = short.Parse(item[4]);
            var labelItem = await _repository.MS_QR_Label.FindSingle(x => x.PurNo.Trim() == PurNo.Trim() && x.ManNo.Trim() == ManNo.Trim() && x.Size.Trim() == Size.Trim() && x.Serial == Serial);
            var sortItem = await _repository.MS_QR_Sort.FindSingle(x => x.PurNo.Trim() == PurNo.Trim() && x.ManNo.Trim() == ManNo.Trim() && x.Serial == Serial && x.Size == Size);
            // if (labelItem == null)
            // {
            //     return new SDCores.OperationResult(false, "LabelNotBePrinted");
            // }
            // else if (sortItem != null)
            // {
            //     return new SDCores.OperationResult(false, "AlreadyScannedIn2.3");
            // }
            // else if (labelItem.Flag == "C")
            // {
            //     return new SDCores.OperationResult(false, "Cancelled");
            // }
            return new OperationResult(true);
        }

        public async Task<PaginationUtility<ViewDataPackingScan>> GetList(PaginationParam paginationParam, string TransactionNo)
        {
              var result = await GetData(TransactionNo);
            result = result.OrderBy(x => x.ManNo).ToList();
            return PaginationUtility<ViewDataPackingScan>.Create(result, paginationParam.PageNumber, paginationParam.PageSize);
        }

        private async Task<List<ViewDataPackingScan>> GetData(string TransactionNo)
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
                        .Select(x => new ViewDataPackingScan
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

        public async Task<List<PackingScanExportDTO>> GetDataExport(string TransactionNo)
        {
            var data = await GetData(TransactionNo);
            data = data.OrderBy(x => x.PurNo).ThenBy(x => x.ManNo).ToList();
            var dataGrouped = data.GroupBy(x => new { x.ManNo, x.PurNo, x.Size }).Select(x => new ViewDataPackingScan
            {
                SDat = x.First().SDat,
                TrNo = x.First().TrNo,
                PrintTime = x.First().PrintTime,
                Shift = x.First().Shift,
                ManNo = x.Key.ManNo,
                PurNo = x.Key.PurNo,
                RModel = x.First().RModel,
                Bitnbr = x.First().Bitnbr,
                Size = x.Key.Size,
                Qty = x.Sum(y => y.Qty),
                CrUsr = x.First().CrUsr
            }).ToList();

            var result = dataGrouped.GroupBy(x => x.ManNo).Select(x => new PackingScanExportDTO
            {
                ManNo = x.Key,
                ListItemPerPage = dataGrouped.Where(y => y.ManNo == x.Key).ToList(),
                Qty = dataGrouped.Where(y => y.ManNo == x.Key).Sum(y => y.Qty)
            }).ToList();

            return result;
        }

    }
}