
using API._Repositories;
using API._Services.Interfaces.Report;
using API.DTOs.Report;
using LinqKit;
using SD3_API.Helpers.Utilities;
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using API.Data;
using Aspose.Cells;

namespace API._Services.Services.Report
{
    public class Report_wksh_SumService : IReport_wksh_SumService
    {
        private readonly IRepositoryAccessor _reposioryAccessor;
        private DBContext _dbContext;
        public Report_wksh_SumService(IRepositoryAccessor repoAccessor, DBContext dbContext)
        {
            _reposioryAccessor = repoAccessor;
            _dbContext = dbContext;
        }
        public async Task<PaginationUtility<Report_wksh_SumParam>> GetData(PaginationParam pagination, Report_wksh_SumResult_Param param, bool isPaging = true)
        {
            var data = await _dbContext.Report_Wksh_SumResult.FromSqlRaw
             ("EXEC Report_wksh_Sum @p_mdat_start, @p_mdat_end, @p_close_status, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
             new SqlParameter("p_mdat_start", param.mdat_dateFrom.HasValue ? param.mdat_dateFrom.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_mdat_end", param.mdat_dateTo.HasValue ? param.mdat_dateTo.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_close_status", !string.IsNullOrEmpty(param.close_status) ? param.close_status : (object)DBNull.Value),
             new SqlParameter("p_brand", !string.IsNullOrEmpty(param.brandname) ? param.brandname : (object)DBNull.Value),
             new SqlParameter("p_code_of_customer", !string.IsNullOrEmpty(param.cusna) ? param.cusna : (object)DBNull.Value),
             new SqlParameter("p_planning_no", !string.IsNullOrEmpty(param.manno) ? param.manno : (object)DBNull.Value),
             new SqlParameter("p_mold_num", !string.IsNullOrEmpty(param.rmodel) ? param.rmodel : (object)DBNull.Value),
             new SqlParameter("p_tolcls", !string.IsNullOrEmpty(param.tolcls) ? param.tolcls : (object)DBNull.Value),
             new SqlParameter("p_purchase_no", !string.IsNullOrEmpty(param.purno) ? param.purno : (object)DBNull.Value),
             new SqlParameter("p_material_code", !string.IsNullOrEmpty(param.bitnbr) ? param.bitnbr : (object)DBNull.Value),
             new SqlParameter("p_kind", !string.IsNullOrEmpty(param.kind) ? param.kind : (object)DBNull.Value),
             new SqlParameter("p_etd_start", param.eta_dateFrom.HasValue ? param.eta_dateFrom.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_etd_end", param.eta_dateTo.HasValue ? param.eta_dateTo.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_size", !string.IsNullOrEmpty(param.size) ? param.size : (object)DBNull.Value)
             ).ToListAsync();
            return PaginationUtility<Report_wksh_SumParam>.Create(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }
         public async Task<List<getBrand>> GetBrand()
        {

            return await _reposioryAccessor.MS_QR_Order.FindAll().Select(x=>new getBrand{
                brandname = x.brandname,
                id = x.brandname
            }).Distinct().ToListAsync();
        }

        //#region ExportExcel - Xuất dữ liệu excel
  

        // private async Task<List<Report_wksh_SumParam>> GetDatas(Report_wksh_SumParam param)
        // {
        //     var query = await _reposioryAccessor.MS_QR_Order.FindAll()
        //                 .Join(_reposioryAccessor.MS_QR_Storage.FindAll(),
        //                     order => new { order.manuf, order.manno, order.size, order.purno },
        //                     storage => new { storage.manuf, storage.manno, storage.size, storage.purno },
        //                     (x, y) => new { order = x, storage = y })
        //                  .Join(_reposioryAccessor.MS_QR_Sort.FindAll(),
        //                     x => new { Manuf = x.storage.manuf, x.storage.QRCodeID },
        //                     sort => new { sort.Manuf, sort.QRCodeID },
        //                     (x, sort) => new { order = x.order, storage = x.storage, sort = sort })
        //                  .Select(x => new Report_wksh_SumParam()
        //                  {
        //                      mdat = x.order.mdat,
        //                      is_close = x.storage.qty - x.order.qty == 0 ? "Y": "N",
        //                      brandname = x.order.brandname,
        //                      cusid = x.order.cusid,
        //                      cusna = x.order.cusna,
        //                      manno = x.order.manno,
        //                      purno = x.order.purno,
        //                      rmodel = x.order.rmodel,
        //                      tolcls = x.order.tolcls,
        //                      ritnbr = x.order.ritnbr,
        //                      bitnbr = x.order.bitnbr,
        //                      kind = x.order.kind,
        //                      article = x.order.article,
        //                      eta = x.order.eta,
        //                      size = x.order.size,
        //                      qty = x.order.qty,
        //                      wkshqty = x.order.wkshqty,
        //                      pqty = x.order.pqty,
        //                      sort_qty = x.sort.Qty,
        //                      storage_qty = x.storage.qty,
        //                      diff_qty = x.sort.Qty - x.storage.qty,
        //                      cqty = x.order.cqty
        //                  }).ToListAsync();
        //     return query;
        // }
        // public async Task<byte[]> ExportExcel(PaginationParam pagination, Report_wksh_SumResult_Param param, bool isPaging = true)
        // {
        //     List<Report_wksh_SumParam> data = await GetData(pagination, param, false);
        //     MemoryStream stream = new MemoryStream();
        //     if (data.Any())
        //     {
        //         var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources\\Template\\Report\\4.1Report_wksh_Sum\\Report_wksh_Sum.xlsx");
        //         WorkbookDesigner designer = new WorkbookDesigner();
        //         designer.Workbook = new Workbook(path);
        //         Worksheet ws = designer.Workbook.Worksheets[0];
        //         designer.SetDataSource("query", data);
        //         designer.Process();
        //         designer.Workbook.Save(stream, SaveFormat.Xlsx);
        //     }
        //     return stream.ToArray();
        // }
        // #endregion
    }
}
