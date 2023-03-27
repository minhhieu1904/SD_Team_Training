using API._Repositories;
using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.PackingScan;
using API.Dtos.Transaction.ReprintPackingScan;
using API.Helpers.Params.Transaction;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Transaction
{
    public class S_ReprintPackingScanService : I_ReprintPackingScanService
    {
        private readonly IConfiguration _config;
        public readonly IRepositoryAccessor _repository;
        protected readonly string _manuf;
        public S_ReprintPackingScanService(IRepositoryAccessor repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
            _manuf = _config.GetSection("Appsettings:FactoryCode").Value;
        }

        public async Task<PaginationUtility<ReprintPackingScanModel>> GetData(PaginationParam param, ReprintPackingScanParam filterParam)
        {
            var predMS_QR_Sort = PredicateBuilder.New<MS_QR_Sort>(true);
            if (!string.IsNullOrWhiteSpace(filterParam.StartDate) && !string.IsNullOrWhiteSpace(filterParam.EndDate))
            {
                var startTime = Convert.ToDateTime(filterParam.StartDate + "00:00:00.000");
                var endTime = Convert.ToDateTime(filterParam.EndDate + "23:59:59.997");
                predMS_QR_Sort.And(x => x.SDat >= startTime && x.SDat <= endTime);
            }
            if (!string.IsNullOrWhiteSpace(filterParam.trno))
            {
                predMS_QR_Sort.And(x => x.TrNo.Trim() == filterParam.trno.Trim());
            }
            if (!string.IsNullOrWhiteSpace(filterParam.shift))
            {
                predMS_QR_Sort.And(x => x.Shift.Trim() == filterParam.shift);
            }
            if (!string.IsNullOrWhiteSpace(filterParam.ManNo))
            {
                predMS_QR_Sort.And(x => x.ManNo.Trim() == filterParam.ManNo.Trim());
            }
            if (!string.IsNullOrWhiteSpace(filterParam.PurNo))
            {
                predMS_QR_Sort.And(x => x.PurNo.Trim() == filterParam.PurNo.Trim());
            }
            if (!string.IsNullOrWhiteSpace(filterParam.Size))
            {
                predMS_QR_Sort.And(x => x.Size.Trim() == filterParam.Size.Trim());
            }

            var sorts = _repository.MS_QR_Sort.FindAll(predMS_QR_Sort);
            var shifts = _repository.MS_Shift.FindAll();
            var data = sorts.GroupJoin(shifts,
                        x => new { x.Shift },
                        y => new { y.Shift },
                        (x, y) => new { sort = x, shift = y })
                        .SelectMany(x => x.shift.DefaultIfEmpty(),
                        (x, y) => new { x.sort, shift = y })
                        .OrderBy(x => x.sort.TrNo)
                        .GroupBy(x => new
                        {
                            x.sort.SDat,
                            x.sort.TrNo,
                            x.shift.ShiftName,
                            x.sort.ManNo,
                            x.sort.PurNo,
                            x.sort.Size
                        }).Select(x => new ReprintPackingScanModel()
                        {
                            SDat = x.Key.SDat,
                            TrNo = x.Key.TrNo,
                            SortQty = x.Sum(y => y.sort.Qty),
                            ShiftName = x.Key.ShiftName,
                            manno = x.Key.ManNo,
                            purno = x.Key.PurNo,
                            size = x.Key.Size,
                            Shift = x.Max(y => y.shift.Shift),
                            CrUsr = x.Max(y => y.sort.CrUsr)
                        }).AsNoTracking();
            return await PaginationUtility<ReprintPackingScanModel>.CreateAsync(data, param.PageNumber, param.PageSize);
        }

        public async Task<List<PackingScanExportDto>> GetExportData(List<ReprintPackingScanModel> listTrNo)
        {
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            var data = listTrNo.OrderBy(x => x.TrNo).ThenBy(x => x.purno).ThenBy(x => x.manno)
                                .GroupBy(x => new { x.TrNo, x.manno })
                                .Select(x => new PackingScanExportDto
                                {
                                    ManNo = x.Key.manno,
                                    ListItemPerPage = x.GroupBy(x => new { x.manno, x.purno, x.size })
                                                        .Select(x => new PackingScanViewDto
                                                        {
                                                            SDat = x.First().SDat.ToString("yyyy/MM/dd"),
                                                            TrNo = x.First().TrNo,
                                                            PrintTime = currentDate,
                                                            Shift = x.First().Shift,
                                                            ManNo = x.Key.manno,
                                                            PurNo = x.Key.purno,
                                                            Qty = x.Max(y => y.SortQty),
                                                            CrUsr = x.First().CrUsr
                                                        }).ToList(),
                                    Qty = x.Sum(y => y.SortQty)
                                }).ToList();

            return await Task.FromResult(data);
        }
    }
}