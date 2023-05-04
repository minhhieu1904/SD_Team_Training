

using API._Repositories;
using API._Services.Interfaces.report;
using API.Data;
using API.DTOs.report.SortSumReport;
using API.Models;
using API.Models.report;
using LinqKit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.report
{
    public class S_SortSumReport : I_SortSumReport
    {
        public readonly IRepositoryAccessor _reposioryAccessor;
        public DBContext _dbContext;
        public S_SortSumReport(IRepositoryAccessor reposioryAccessor, DBContext dbContext){
           _reposioryAccessor = reposioryAccessor;
            _dbContext = dbContext;
        }
        public async Task<PaginationUtility<Report_Sort_SumResult>> GetData(PaginationParam pagination, SortSumReport param, bool isPaging = true)
        {
            var data = await _dbContext.Report_Sort_SumResult.FromSqlRaw
            ("EXEC Report_Sort_Sum @p_date_kind, @p_date_start, @p_date_end, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
            new SqlParameter("p_date_kind", !string.IsNullOrEmpty(param.date_kind) ? param.date_kind : (object)DBNull.Value),
            new SqlParameter("p_date_start",param.date_start.HasValue ? param.date_start.Value.Date :  (object)DBNull.Value),
            new SqlParameter("p_date_end",param.date_end.HasValue ? param.date_end.Value.Date :  (object)DBNull.Value),
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
            return PaginationUtility<Report_Sort_SumResult>.Create(data,pagination.PageNumber,pagination.PageSize,isPaging);
        }
        public async Task<List<getBrand>> GetBrand()
        {

            return await _reposioryAccessor.MS_QR_Order.FindAll().Select(x=>new getBrand{
                brandname = x.Brandname,
                id = x.Brandname
            }).Distinct().ToListAsync();
        }

        public async Task<List<SortSumReportDetail>> GetDataDetail(SortSumDetailReportParam param)
        {
            var pred_MS_QR_Label = PredicateBuilder.New<MS_QR_Label>(true);
            if(!string.IsNullOrWhiteSpace(param.manno)) 
                pred_MS_QR_Label.And(x => x.ManNo == param.manno);
            if(!string.IsNullOrWhiteSpace(param.purno)){
                pred_MS_QR_Label.And(x => x.PurNo == param.purno);
            }
            if(!string.IsNullOrWhiteSpace(param.size)){
                pred_MS_QR_Label.And(x => x.Size == param.size);
            }
            var data = await _reposioryAccessor.MS_QR_Label.FindAll(pred_MS_QR_Label)
                        .GroupJoin(_reposioryAccessor.MS_QR_Order.FindAll(), 
                            l => new { ManNo = l.ManNo,PurNo= l.PurNo,Size= l.Size },
                            s => new { ManNo = s.Manno, PurNo = s.Purno, Size = s.Size },
                            (l,s) => new { T1 = l, T2 = s }
                        ).SelectMany(x => x.T2.DefaultIfEmpty(), (x,y) => new { x.T1, T2 = y }) 
                        .GroupJoin(_reposioryAccessor.MS_QR_Sort.FindAll(),
                            x => x.T1.QRCodeID,
                            y => y.QRCodeID,
                            (x,y) => new { x.T1, x.T2, T3 = y}
                        ).SelectMany(x => x.T3.DefaultIfEmpty(), (x,y) => new { x.T1, x.T2, T3 = y})
                        .Select(x => new SortSumReportDetail(){
                            IsScanSort = x.T3.QRCodeID != null ? "Y" : "N",
                            CrDay = x.T3.CrDay.ToString("dd/MM/yyyy"),
                            brandname = x.T2.Brandname,
                            QRCodeID = x.T1.QRCodeID,
                            ManNo = x.T1.ManNo,
                            PurNo = x.T1.PurNo,
                            Size = x.T1.Size,
                            Serial = x.T1.Serial,
                            Qty = x.T1.Qty,
                            cusna = x.T2.Cusna,
                            rmodel = x.T2.Rmodel,
                            tolcls = x.T2.Tolcls,
                            ritnbr = x.T2.Ritnbr,
                            bitnbr = x.T2.Bitnbr,
                            article= x.T2.Article,
                            kind = x.T2.Kind,
                            eta = x.T2.Eta
                        }).ToListAsync();
            return data;
            
            
        }
    }
}