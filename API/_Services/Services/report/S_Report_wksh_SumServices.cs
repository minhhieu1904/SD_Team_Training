using API._Repositories;
using API._Services.Interfaces;
using API.Data;
using API.DTOs;
using API.DTOs.Report;
using API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services
{
    public class S_Report_wksh_SumServices : I_Report_wksh_SumServices
    {
        private readonly IRepositoryAccessor _repoAccessor;
        public DBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public S_Report_wksh_SumServices(IRepositoryAccessor repoAccessor, DBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _repoAccessor = repoAccessor;
            _dbContext = dbContext;
        }

        public async Task<List<Report_Wksh_SumResult>> GetData(ReportWkshSum model)
        {
            return await _dbContext.Report_Wksh_SumResult.FromSqlRaw
          ("EXEC Report_wksh_Sum @p_mdat_start, @p_mdat_end, @p_close_status, @p_brand, @p_code_of_customer, @p_planing_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
          new SqlParameter("p_mdat_start", model.mdat_start.HasValue ? model.mdat_start.Value.Date : (object)DBNull.Value),
          new SqlParameter("p_mdat_end", model.mdat_end.HasValue ? model.mdat_end.Value.Date : (object)DBNull.Value),
          new SqlParameter("p_close_status", !string.IsNullOrEmpty(model.close_status) ? model.close_status : (object)DBNull.Value),
          new SqlParameter("p_brand", !string.IsNullOrEmpty(model.brandname) ? model.brandname : (object)DBNull.Value),
          new SqlParameter("p_code_of_customer", !string.IsNullOrEmpty(model.cusna) ? model.cusna : (object)DBNull.Value),
          new SqlParameter("p_planing_no", !string.IsNullOrEmpty(model.manno) ? model.manno : (object)DBNull.Value),
          new SqlParameter("p_mold_num", !string.IsNullOrEmpty(model.rmodel) ? model.rmodel : (object)DBNull.Value),
          new SqlParameter("p_tolcls", !string.IsNullOrEmpty(model.tolcls) ? model.tolcls : (object)DBNull.Value),
          new SqlParameter("p_purchase_no", !string.IsNullOrEmpty(model.purno) ? model.purno : (object)DBNull.Value),
          new SqlParameter("p_material_code", !string.IsNullOrEmpty(model.bitnbr) ? model.bitnbr : (object)DBNull.Value),
          new SqlParameter("p_kind", !string.IsNullOrEmpty(model.kind) ? model.kind : (object)DBNull.Value),
          new SqlParameter("p_etd_start", model.eta_start.HasValue ? model.eta_start.Value.Date : (object)DBNull.Value),
          new SqlParameter("p_etd_end", model.eta_end.HasValue ? model.eta_end.Value.Date : (object)DBNull.Value),
          new SqlParameter("p_size", !string.IsNullOrEmpty(model.size) ? model.size : (object)DBNull.Value)
          ).ToListAsync();
        }

        public async Task<PaginationUtility<Report_Wksh_SumResult>> GetDataPaging(PaginationParam param, ReportWkshSum model)
        {
            var data = await GetData(model);
            return PaginationUtility<Report_Wksh_SumResult>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<List<GetBrand>> GetBrands()
        {
            return await _repoAccessor.MS_QR_Order.FindAll().Select(x => new GetBrand
            {
                brandname = x.brandname,
                id = x.brandname
            }).Distinct().ToListAsync();
        }

        public async Task<OperationResult> ExportExcel(ReportWkshSum param, string userName)
        {
            var data = await GetData(param);
            if (!data.Any())
            {
                return new OperationResult(false, "No data");
            }
            List<Table> listTable = new List<Table>(){
                new Table ("result", data)
            };
            List<Cell> listCell = new List<Cell>() {
                new Cell ("B1", userName),
                new Cell ("B2", DateTime.Now.ToString("yyyy/mm/dd")),
            };
            ExcelResult result = ExcelUtility.DownloadExcel(listTable, listCell, "Resources/Template/Report/wkshSumReport.xlsx");
            return new OperationResult(result.IsSuccess, result.Error, result.Result);
        }
    }
}