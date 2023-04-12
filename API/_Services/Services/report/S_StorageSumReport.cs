using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.report;
using API.Data;
using API.DTOs.report.SortSumReport;
using API.Models;
using LinqKit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.report
{
    public class S_StorageSumReport : I_StorageSumReport
    {
        public readonly IRepositoryAccessor _reposioryAccessor;
        public DBContext _dbContext;
        public S_StorageSumReport(IRepositoryAccessor reposioryAccessor, DBContext dbContext){
           _reposioryAccessor = reposioryAccessor;
            _dbContext = dbContext;
        }

        public async Task<PaginationUtility<Report_Storage_SumResult>> GetData(PaginationParam pagination, StorageSumReport param, bool isPaging = true)
        {
            var data = await _dbContext.Report_Storage_SumResult.FromSqlRaw
            ("EXEC Report_Storage_Sum @p_date_kind, @p_date_start, @p_date_end, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
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
            return PaginationUtility<Report_Storage_SumResult>.Create(data,pagination.PageNumber,pagination.PageSize,isPaging);
        }
        public async Task<List<getBrand>> GetBrand()
        {

            return await _reposioryAccessor.MS_QR_Order.FindAll().Select(x=>new getBrand{
                brandname = x.brandname,
                id = x.brandname
            }).Distinct().ToListAsync();
        }

        public async Task<List<StorageSumReportDetai>> GetDataDetail(StorageSumReportDetailParam param)
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
                            s => new { ManNo = s.manno, PurNo = s.purno, Size = s.size },
                            (l,s) => new { T1 = l, T2 = s }
                        ).SelectMany(x => x.T2.DefaultIfEmpty(), (x,y) => new { x.T1, T2 = y }) 
                        .GroupJoin(_reposioryAccessor.MS_QR_Sort.FindAll(),
                            x => x.T1.QRCodeID,
                            y => y.QRCodeID,
                            (x,y) => new { x.T1, x.T2, T3 = y}
                        ).SelectMany(x => x.T3.DefaultIfEmpty(), (x,y) => new { x.T1, x.T2, T3 = y})
                        .Select(x => new StorageSumReportDetai(){
                            IsStorageSort = x.T3.QRCodeID != null ? "Y" : "N",
                            CrDay = x.T3.CrDay.ToString("dd/MM/yyyy"),
                            brandname = x.T2.brandname,
                            QRCodeID = x.T1.QRCodeID,
                            ManNo = x.T1.ManNo,
                            PurNo = x.T1.PurNo,
                            Size = x.T1.Size,
                            Serial = x.T1.Serial,
                            Qty = x.T1.Qty,
                            cusna = x.T2.cusna,
                            rmodel = x.T2.rmodel,
                            tolcls = x.T2.tolcls,
                            ritnbr = x.T2.ritnbr,
                            bitnbr = x.T2.bitnbr,
                            article= x.T2.article,
                            kind = x.T2.kind,
                            eta = x.T2.eta
                        }).ToListAsync();
            return data;
            
            
        }
    }
}