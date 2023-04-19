
using API._Repositories;
using API._Services.Interfaces.Report;
using API.Data;
using API.DTOs.Report;
using API.Models;
using Aspose.Cells;
using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Report
{
    public class Report_Storage_Sumservice : IReport_Storage_Sumservice
    {
        private readonly IRepositoryAccessor _reposioryAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private DBContext _dbContext;

        public Report_Storage_Sumservice(IRepositoryAccessor reposioryAccessor, IWebHostEnvironment webHostEnvironment, DBContext dbContext)
        {
            _reposioryAccessor = reposioryAccessor;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }
        #region ExportExcel - Xuất dữ liệu excel

        public async Task<byte[]> ExportExcelDetails(StorageSumDeltailDTOParam param)
        {
            List<StorageSumDeltailDTO> data = await GetDataDetails(param);
            MemoryStream stream = new MemoryStream();
            if (data.Any())
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources\\Template\\Report\\4.3Report_Storage_Sum\\Report_Storage_Sums_Details.xlsx");
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];
                ws.Cells[1, 1].Value = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:sstt");
                designer.SetDataSource("result", data);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            return stream.ToArray();
        }
        private async Task<List<StorageSumDeltailDTO>> GetDataDetails(StorageSumDeltailDTOParam param)
        {
            var predicate = PredicateBuilder.New<MS_QR_Label>(true);
            if (!string.IsNullOrEmpty(param.manno))
                predicate.And(x => x.ManNo.Trim() == param.manno.Trim());
            if (!string.IsNullOrEmpty(param.purno))
                predicate.And(x => x.PurNo.Trim() == param.purno.Trim());
            if (!string.IsNullOrEmpty(param.size))
                predicate.And(x => x.Size.Trim() == param.size.Trim());
            var result = await _reposioryAccessor.MS_QR_Label.FindAll(predicate)
                        .GroupJoin(_reposioryAccessor.MS_QR_Order.FindAll().AsNoTracking(),
                            x => new { x.Manuf, x.ManNo, x.Size, x.PurNo },
                            y => new { Manuf = y.manuf, ManNo = y.manno, Size = y.size, PurNo = y.purno },
                             (x, y) => new { T1 = x, T2 = y })
                        .SelectMany(x => x.T2.DefaultIfEmpty(), (t, y) => new { T1 = t.T1, T2 = y })
                        .GroupJoin(_reposioryAccessor.MS_QR_Storage.FindAll().AsNoTracking(),
                        x => x.T1.QRCodeID,
                        y => y.QRCodeID,
                        (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y })
                        .SelectMany(x => x.T3.DefaultIfEmpty(), (x, y) => new { T1 = x.T1, T2 = x.T2, T3 = y })
                        .Select(x => new StorageSumDeltailDTO()
                        {
                            IsStorageSort = x.T1.QRCodeID != null ? "Y" : "N",
                            CrDay = x.T3.crday,
                            brandname = x.T2.brandname,
                            QRCodeID = x.T1.QRCodeID,
                            manno = x.T1.ManNo,
                            purno = x.T1.PurNo,
                            size = x.T1.Size,
                            serial = x.T1.Serial,
                            storage_Qty = x.T1.Qty,
                            cusid = x.T2.cusid,
                            cusna = x.T2.cusna,
                            rmodel = x.T2.rmodel,
                            tolcls = x.T2.tolcls,
                            ritnbr = x.T2.ritnbr,
                            bitnbr = x.T2.bitnbr,
                            article = x.T2.article,
                            kind = x.T2.kind,
                            eta = x.T2.eta,
                        }).ToListAsync();
            return result;
        }

        #endregion
        public async Task<List<getBrand>> GetBrand()
        {
            return await _reposioryAccessor.MS_QR_Order.FindAll().Select(x => new getBrand
            {
                brandname = x.brandname,
                id = x.brandname
            }).Distinct().ToListAsync();
        }

        public async Task<PaginationUtility<Report_Storage_SumParam>> GetData(PaginationParam pagination, Storage_sumDTO param, bool isPaging = true)
        {
            var data = await _dbContext.Report_Storage_SumParam.FromSqlRaw
           ("EXEC Report_Storage_Sum @p_date_kind,@p_date_start,@p_date_end,@p_brand,@p_code_of_customer,@p_planning_no,@p_mold_num,@p_tolcls,@p_purchase_no,@p_material_code,@p_kind,@p_etd_start,@p_etd_end,@p_size",
               new SqlParameter("p_date_kind", !string.IsNullOrEmpty(param.date_kind) ? param.date_kind : (object)DBNull.Value),
               new SqlParameter("p_date_start", param.date_start.HasValue ? param.date_start.Value.Date : (object)DBNull.Value),
               new SqlParameter("p_date_end", param.date_end.HasValue ? param.date_end.Value.Date : (object)DBNull.Value),
               new SqlParameter("p_brand", !string.IsNullOrEmpty(param.brandname) ? param.brandname : (object)DBNull.Value),
               new SqlParameter("p_code_of_customer", !string.IsNullOrEmpty(param.cusna) ? param.cusna : (object)DBNull.Value),
               new SqlParameter("p_planning_no", !string.IsNullOrEmpty(param.manno) ? param.manno : (object)DBNull.Value),
               new SqlParameter("p_mold_num", !string.IsNullOrEmpty(param.rmodel) ? param.rmodel : (object)DBNull.Value),
               new SqlParameter("p_tolcls", !string.IsNullOrEmpty(param.tolcls) ? param.tolcls : (object)DBNull.Value),
               new SqlParameter("p_purchase_no", !string.IsNullOrEmpty(param.purno) ? param.purno : (object)DBNull.Value),
               new SqlParameter("p_material_code", !string.IsNullOrEmpty(param.bitnbr) ? param.bitnbr : (object)DBNull.Value),
               new SqlParameter("p_kind", !string.IsNullOrEmpty(param.kind) ? param.kind : (object)DBNull.Value),
               new SqlParameter("p_etd_start", param.eta_start.HasValue ? param.eta_start.Value.Date : (object)DBNull.Value),
               new SqlParameter("p_etd_end", param.eta_end.HasValue ? param.eta_end.Value.Date : (object)DBNull.Value),
               new SqlParameter("p_size", !string.IsNullOrEmpty(param.size) ? param.size : (object)DBNull.Value)
               ).ToListAsync();
            return PaginationUtility<Report_Storage_SumParam>.Create(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }
    }
}