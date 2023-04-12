using API._Repositories;
using API._Services.Interfaces.Report;
using API.Data;
using API.Dtos.Report.Report_Sort_Sum;
using API.DTOs.Report;
using API.Helpers;
using LinqKit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Report
{
    public class S_StorageSumReport : I_StorageSumReport
    {
        private readonly MyDBContext _dbContext;
        private readonly IRepositoryAccessor _repositoryAccessor;
        public S_StorageSumReport(MyDBContext dbContext, IRepositoryAccessor repositoryAccessor)
        {
            _dbContext = dbContext;
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<PaginationUtility<Report_Storage_SumResult>> Search(PaginationParam pagination, Report_Storage_Sum_Param param, bool isPaging = true)
        {
            var data = await _dbContext.Report_Storage_SumResult
                .FromSqlRaw("EXEC Report_Storage_Sum @p_date_kind,@p_date_start,@p_date_end,@p_brand,@p_code_of_customer,@p_planning_no,@p_mold_num,@p_tolcls,@p_purchase_no,@p_material_code,@p_kind,@p_etd_start,@p_etd_end,@p_size",
                new SqlParameter("@p_date_kind", !string.IsNullOrEmpty(param.crday) ? param.crday : (object)DBNull.Value),
                new SqlParameter("@p_date_start", !string.IsNullOrEmpty(param.sdat) ? param.sdat : (object)DBNull.Value),
                new SqlParameter("@p_date_end", !string.IsNullOrEmpty(param.mdat) ? param.mdat : (object)DBNull.Value),
                new SqlParameter("@p_brand", !string.IsNullOrEmpty(param.brandname) ? param.brandname : (object)DBNull.Value),
                new SqlParameter("@p_code_of_customer", !string.IsNullOrEmpty(param.cusna) ? param.cusna : (object)DBNull.Value),
                new SqlParameter("@p_planning_no", !string.IsNullOrEmpty(param.manno) ? param.manno : (object)DBNull.Value),
                new SqlParameter("@p_mold_num", !string.IsNullOrEmpty(param.rmodel) ? param.rmodel : (object)DBNull.Value),
                new SqlParameter("@p_tolcls", !string.IsNullOrEmpty(param.tolcls) ? param.tolcls : (object)DBNull.Value),
                new SqlParameter("@p_purchase_no", !string.IsNullOrEmpty(param.purno) ? param.purno : (object)DBNull.Value),
                new SqlParameter("@p_material_code", !string.IsNullOrEmpty(param.bitnbr) ? param.bitnbr : (object)DBNull.Value),
                new SqlParameter("@p_kind", !string.IsNullOrEmpty(param.kind) ? param.kind : (object)DBNull.Value),
                new SqlParameter("@p_etd_start", !string.IsNullOrEmpty(param.startDate) ? param.startDate : (object)DBNull.Value),
                new SqlParameter("@p_etd_end", !string.IsNullOrEmpty(param.endDate) ? param.endDate : (object)DBNull.Value),
                new SqlParameter("@p_size", !string.IsNullOrEmpty(param.size) ? param.size : (object)DBNull.Value)
                ).ToListAsync();
            return PaginationUtility<Report_Storage_SumResult>.Create(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<List<Export_Detail_Excel>> ExportDetailExcel(ExportDetailExcelParams param)
        {
            var pred = PredicateBuilder.New<MS_QR_Label>(true);
            if (!string.IsNullOrEmpty(param.manno))
                pred.And(x => x.ManNo == param.manno);
            if (!string.IsNullOrEmpty(param.purno))
                pred.And(x => x.PurNo == param.purno);
            if (!string.IsNullOrEmpty(param.size))
                pred.And(x => x.Size == param.size);
            var labels = _repositoryAccessor.MS_QR_Label.FindAll(pred);
            var orders = _repositoryAccessor.MS_QR_Order.FindAll();
            var sorts = _repositoryAccessor.MS_QR_Sort.FindAll();

            var data = await labels.GroupJoin(orders,
                                    x => new { ManNo = x.ManNo, PurNo = x.PurNo, Size = x.Size },
                                    y => new { ManNo = y.manno, PurNo = y.purno, Size = y.size },
                                    (x, y) => new { T1 = x, T2 = y }).SelectMany(x => x.T2.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = y })
                                    .GroupJoin(sorts,
                                    x => x.T1.QRCodeID,
                                    y => y.QRCodeID,
                                    (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y })
                                    .SelectMany(x => x.T3.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y })
                                    .Select(x => new Export_Detail_Excel
                                    {
                                        IsStorageSort = (x.T3.QRCodeID != null) ? "Y" : "N",
                                        CrDay = x.T3.CrDay,
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
                                        article = x.T2.article,
                                        kind = x.T2.kind,
                                        eta = x.T2.eta
                                    }).AsNoTracking().ToListAsync();
            return data;
        }
    }
}