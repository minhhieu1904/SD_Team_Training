
using API._Repositories;
using API._Services.Interfaces.report;
using API.Data;
using API.DTOs.report;
using API.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.report
{
    public class S_WkshSumReport : I_WkshSumReport
    {
        public readonly IRepositoryAccessor _reposioryAccessor;
        public DBContext _dbContext;
        public S_WkshSumReport(IRepositoryAccessor reposioryAccessor, DBContext dbContext){
           _reposioryAccessor = reposioryAccessor;
            _dbContext = dbContext;
        }
        public async Task<List<getBrand>> GetBrand()
        {

            return await _reposioryAccessor.MS_QR_Order.FindAll().Select(x=>new getBrand{
                brandname = x.Brandname,
                id = x.Brandname
            }).Distinct().ToListAsync();
        }

        public async Task<PaginationUtility<Report_wksh_SumResult>> GetData(PaginationParam pagination, WkshSumReport param, bool isPaging = true)
        {
            var data = await _dbContext.Report_wksh_SumResult.FromSqlRaw
            ("EXEC Report_wksh_Sum @p_mdat_start, @p_mdat_end, @p_close_status, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
            new SqlParameter("p_mdat_start",param.mdat_start.HasValue ? param.mdat_start.Value.Date :  (object)DBNull.Value),
            new SqlParameter("p_mdat_end",param.mdat_end.HasValue ? param.mdat_end.Value.Date :  (object)DBNull.Value),
            new SqlParameter("p_close_status", !string.IsNullOrEmpty(param.close_status) ? param.close_status : (object)DBNull.Value),
            new SqlParameter("p_brand", !string.IsNullOrEmpty(param.brandname) ? param.brandname : (object)DBNull.Value),
            new SqlParameter("p_code_of_customer", !string.IsNullOrEmpty(param.cusna) ? param.cusna : (object)DBNull.Value),
            new SqlParameter("p_planning_no", !string.IsNullOrEmpty(param.manno) ? param.manno : (object)DBNull.Value),
            new SqlParameter("p_mold_num", !string.IsNullOrEmpty(param.rmodel) ? param.rmodel : (object)DBNull.Value),
            new SqlParameter("p_tolcls", !string.IsNullOrEmpty(param.tolcls) ? param.tolcls : (object)DBNull.Value),
            new SqlParameter("p_purchase_no", !string.IsNullOrEmpty(param.purno) ? param.purno : (object)DBNull.Value),
            new SqlParameter("p_material_code", !string.IsNullOrEmpty(param.bitnbr) ? param.bitnbr : (object)DBNull.Value),
            new SqlParameter("p_kind", !string.IsNullOrEmpty(param.kind) ? param.kind : (object)DBNull.Value),
            new SqlParameter("p_etd_start",param.eta_start.HasValue ? param.eta_start.Value.Date :  (object)DBNull.Value),
            new SqlParameter("p_etd_end",param.eta_end.HasValue ? param.eta_end.Value.Date :  (object)DBNull.Value),
            new SqlParameter("p_size", !string.IsNullOrEmpty(param.size) ? param.size : (object)DBNull.Value)
            ).ToListAsync();
            return PaginationUtility<Report_wksh_SumResult>.Create(data,pagination.PageNumber,pagination.PageSize,isPaging);
        }
        
    }
}