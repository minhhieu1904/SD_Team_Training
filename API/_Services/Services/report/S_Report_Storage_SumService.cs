using API._Repositories;
using API._Services.Interfaces.report;
using API.Data;
using API.DTOs.Report;
using API.Models;
using LinqKit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.report
{
    public class S_Report_Storage_SumService : I_Report_StorageSumService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRepositoryAccessor _repoAccessor;
        public DBContext _dBContext;

        public S_Report_Storage_SumService(IRepositoryAccessor repoAccessor, DBContext dBContext, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _repoAccessor = repoAccessor;
            _dBContext = dBContext;
        }

        public async Task<List<GetBrand>> GetBrand()
        {
            var result = await _repoAccessor.MS_QR_Order.FindAll().Select(x => new GetBrand
            {
                brandname = x.brandname,
                id = x.brandname
            }).Distinct().ToListAsync();
            return result;
        }

        public async Task<List<Report_Storage_SumResult>> GetData(ReportStorageSum model)
        {
            return await _dBContext.Report_Storage_SumResult.FromSqlRaw
           ("EXEC Report_Storage_Sum @p_date_kind, @p_date_start, @p_date_end, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
           new SqlParameter("p_date_kind", model.date_kind.HasValue ? model.date_kind.Value.Date : (object)DBNull.Value),
           new SqlParameter("p_date_start", model.date_start.HasValue ? model.date_start.Value.Date : (object)DBNull.Value),
           new SqlParameter("p_date_end", model.date_end.HasValue ? model.date_end.Value.Date : (object)DBNull.Value),
           new SqlParameter("p_brand", !string.IsNullOrEmpty(model.brandname) ? model.brandname : (object)DBNull.Value),
           new SqlParameter("p_code_of_customer", !string.IsNullOrEmpty(model.cusna) ? model.cusna : (object)DBNull.Value),
           new SqlParameter("p_planning_no", !string.IsNullOrEmpty(model.manno) ? model.manno : (object)DBNull.Value),
           new SqlParameter("p_mold_num", !string.IsNullOrEmpty(model.rmodel) ? model.rmodel : (object)DBNull.Value),
           new SqlParameter("p_tolcls", !string.IsNullOrEmpty(model.tolcls) ? model.tolcls : (object)DBNull.Value),
           new SqlParameter("p_purchase_no", !string.IsNullOrEmpty(model.purno) ? model.purno : (object)DBNull.Value),
           new SqlParameter("p_material_code", !string.IsNullOrEmpty(model.bitnbr) ? model.bitnbr : (object)DBNull.Value),
           new SqlParameter("p_kind", !string.IsNullOrEmpty(model.kind) ? model.kind : (object)DBNull.Value),
           new SqlParameter("p_etd_start", model.etd_start.HasValue ? model.etd_start.Value.Date : (object)DBNull.Value),
           new SqlParameter("p_etd_end", model.etd_end.HasValue ? model.etd_end.Value.Date : (object)DBNull.Value),
           new SqlParameter("p_size", !string.IsNullOrEmpty(model.size) ? model.size : (object)DBNull.Value)
           ).ToListAsync();
        }

        public async Task<List<ReportStorageSumDetail>> GetDataDetail(string manno, string purno, string size)
        {
            var predicateKey = PredicateBuilder.New<MS_QR_Label>(true);
            if (!string.IsNullOrEmpty(manno))
                predicateKey = predicateKey.And(x => x.ManNo == manno);
            if (!string.IsNullOrEmpty(purno))
                predicateKey = predicateKey.And(x => x.PurNo == purno);
            if (!string.IsNullOrEmpty(size))
                predicateKey = predicateKey.And(x => x.Size == size);
            var data = await _repoAccessor.MS_QR_Label.FindAll(predicateKey)
            .GroupJoin(_repoAccessor.MS_QR_Order.FindAll(),
                 x => new { x.Manuf, x.ManNo, x.PurNo, x.Size },
                y => new { Manuf = y.manuf, ManNo = y.manno, PurNo = y.purno, Size = y.size },
                (x, y) => new { T1 = x, T2 = y })
            .SelectMany(x => x.T2.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = y }).

             GroupJoin(_repoAccessor.MS_QR_Sort.FindAll().AsNoTracking(), x => x.T1.QRCodeID, y => y.QRCodeID, (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y }).
             SelectMany(y => y.T3.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y }).
             Select(x => new ReportStorageSumDetail
             {
                 IsStorageSort = x.T3.QRCodeID != null ? "Y" : "N",
                 QrcodeId = x.T1.QRCodeID,
                 Brandname = x.T2.brandname,
                 CrDay = x.T3.CrDay,
                 ManNo = x.T1.ManNo,
                 Size = x.T1.Size,
                 Serial = x.T1.Serial,
                 Qty = x.T1.Qty,
                 Cusna = x.T2.cusna,
                 Rmodel = x.T2.rmodel,
                 Tolcls = x.T2.tolcls,
                 Ritnbr = x.T2.ritnbr,
                 Article = x.T2.article,
                 Kind = x.T2.kind,
                 Eta = x.T2.eta
             }).Distinct().ToListAsync();
            return data;
        }

        public async Task<PaginationUtility<Report_Storage_SumResult>> GetDataPaing(PaginationParam param, ReportStorageSum model)
        {
            var data = await GetData(model);
            return PaginationUtility<Report_Storage_SumResult>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<OperationResult> ExportExcel(ReportStorageSum model, string userName)
        {
            var data = await GetData(model);
            if (!data.Any())
                return new OperationResult(false, "No data");
            List<Table> listTable = new List<Table>()
            {
                new Table("result", data)
            };
            List<Cell> listData = new List<Cell>()
            {
                new Cell ("B1", userName),
                new Cell ("B2", DateTime.Now.ToString("yyyy/mm/dd"))
            };
            ExcelResult result = ExcelUtility.DownloadExcel(listTable, listData, "Resources/Template/Report/ReportStorageSum.xlsx");
            return new OperationResult(result.IsSuccess, result.Error, result.Result);
        }

        public async Task<OperationResult> ExportExcelDetail(string userName, string manno, string purno, string size)
        {
            var data = await GetDataDetail(manno, purno, size);
            if (!data.Any())
                return new OperationResult(false, "No data");
            List<Table> listTable = new List<Table>()
            {
                new Table ("result", data)
            };
            List<Cell> listCell = new List<Cell>(){
                new Cell ("B1", userName),
                new Cell ("B2", DateTime.Now.ToString("yyyy/mm/dd"))
            };
            ExcelResult result = ExcelUtility.DownloadExcel(listTable, listCell, "Resources/Template/Report/StorageDetailReport.xlsx");
            return new OperationResult(result.IsSuccess, result.Error, result.Result);
        }
    }
}