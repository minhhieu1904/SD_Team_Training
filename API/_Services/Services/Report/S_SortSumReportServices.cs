
using API._Repositories;
using API._Services.Interfaces.Report;
using API.Data;
using API.DTOs;
using API.DTOs.Report;
using API.Models;
using Aspose.Cells;
using LinqKit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Report
{
    public class S_SortSumReportServices : I_SortSumReportServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        private readonly IWebHostEnvironment _environment;
        public DBContext _dbContext;

        public S_SortSumReportServices(IRepositoryAccessor repositoryAccessor, DBContext dbContext, IWebHostEnvironment environment)
        {
            _repositoryAccessor = repositoryAccessor;
            _environment = environment;
            _dbContext = dbContext;
        }

        public async Task<List<SortSumReportDTO>> GetData(SortSumReportParam param)
        {
            var query = await _dbContext.SortSumReports.FromSqlRaw
             ("EXEC Report_Sort_Sum @p_date_kind, @p_date_start, @p_date_end, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
             new SqlParameter("p_date_kind", !string.IsNullOrEmpty(param.date_kind) ? param.date_kind : (object)DBNull.Value),
             new SqlParameter("p_date_start", param.date_start.HasValue ? param.date_start.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_date_end", param.date_end.HasValue ? param.date_end.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_brand", !string.IsNullOrEmpty(param.brand) ? param.brand : (object)DBNull.Value),
             new SqlParameter("p_code_of_customer", !string.IsNullOrEmpty(param.cusid) ? param.cusid : (object)DBNull.Value),
             new SqlParameter("p_planning_no", !string.IsNullOrEmpty(param.manno) ? param.manno : (object)DBNull.Value),
             new SqlParameter("p_mold_num", !string.IsNullOrEmpty(param.rmodel) ? param.rmodel : (object)DBNull.Value),
             new SqlParameter("p_tolcls", !string.IsNullOrEmpty(param.tolcls) ? param.tolcls : (object)DBNull.Value),
             new SqlParameter("p_purchase_no", !string.IsNullOrEmpty(param.purchase_no) ? param.purchase_no : (object)DBNull.Value),
             new SqlParameter("p_material_code", !string.IsNullOrEmpty(param.material) ? param.material : (object)DBNull.Value),
             new SqlParameter("p_kind", !string.IsNullOrEmpty(param.kind) ? param.kind : (object)DBNull.Value),
             new SqlParameter("p_etd_start", param.etd_start.HasValue ? param.etd_start.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_etd_end", param.etd_end.HasValue ? param.etd_end.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_size", !string.IsNullOrEmpty(param.size) ? param.size : (object)DBNull.Value)
             ).ToListAsync();
            return query;
        }

        public async Task<PaginationUtility<SortSumReportDTO>> GetDataPagination(PaginationParam pagination, SortSumReportParam param)
        {
            var data = await GetData(param);
            return PaginationUtility<SortSumReportDTO>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<List<BrandDTO>> GetBrands()
        {
            return await _repositoryAccessor.MS_QrOrder.FindAll()
            .Select(x => new BrandDTO { brandname = x.Brandname, id = x.Brandname }).Distinct().ToListAsync();
        }

        public async Task<byte[]> ExportExcel(SortSumReportParam param, string userName)
        {
            var data = await GetData(param);

            MemoryStream stream = new MemoryStream();
            if (data.Count > 0)
            {
                var path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\SortSumReport.xlsx");

                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];


                ws.Cells["B1"].PutValue(userName);
                ws.Cells["B2"].PutValue(DateTime.Now);
                designer.SetDataSource("result", data);
                designer.Process();

                ws.AutoFitColumns();
                ws.PageSetup.CenterHorizontally = true;
                ws.PageSetup.FitToPagesWide = 1;
                ws.PageSetup.FitToPagesTall = 0;

                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            return stream.ToArray();
        }

        public async Task<byte[]> ExportDetailExcel(SortSumDetailReportParam param, string userName)
        {
            var pred = PredicateBuilder.New<MsQrLabel>(true);

            if (!string.IsNullOrEmpty(param.manno))
                pred.And(x => x.ManNo.Trim() == param.manno.Trim());

            if (!string.IsNullOrEmpty(param.purno))
                pred.And(x => x.PurNo.Trim() == param.purno.Trim());

            if (!string.IsNullOrEmpty(param.size))
                pred.And(x => x.Size.Trim() == param.size.Trim());

            var data = await _repositoryAccessor.MS_QrLabel.FindAll(pred)
                                    .GroupJoin(_repositoryAccessor.MS_QrOrder.FindAll(),
                                        l => new { ManNo = l.ManNo, PurNo = l.PurNo, Size = l.Size },
                                        s => new { ManNo = s.Manno, PurNo = s.Purno, Size = s.Size },
                                        (l, s) => new { T1 = l, T2 = s }
                                    ).SelectMany(x => x.T2.DefaultIfEmpty(), (x, y) => new { x.T1, T2 = y })
                                    .GroupJoin(_repositoryAccessor.MS_QrSort.FindAll().AsNoTracking(),
                                        x => x.T1.QRCodeID,
                                        y => y.QrcodeId,
                                        (x, y) => new { x.T1, x.T2, T3 = y }
                                    ).SelectMany(x => x.T3.DefaultIfEmpty(), (x, y) => new { x.T1, x.T2, T3 = y })
                                    .Select(x => new SortSumDetailDTO()
                                    {
                                        IsScanSort = x.T3.QrcodeId != null ? "Y" : "N",
                                        CrDay = x.T3.CrDay.ToString("dd/MM/yyyy"),
                                        BrandName = x.T2.Brandname,
                                        QRCodeID = x.T1.QRCodeID,
                                        ManNo = x.T1.ManNo,
                                        PurNo = x.T1.PurNo,
                                        Size = x.T1.Size,
                                        Serial = x.T1.Serial,
                                        Qty = x.T1.Qty,
                                        Cusna = x.T2.Cusna,
                                        Rmodel = x.T2.Rmodel,
                                        Tolcls = x.T2.Tolcls,
                                        Ritnbr = x.T2.Ritnbr,
                                        Bitnbr = x.T2.Bitnbr,
                                        Article = x.T2.Article,
                                        Kind = x.T2.Kind,
                                        Eta = x.T2.Eta
                                    }).ToListAsync();

            MemoryStream stream = new MemoryStream();
            if (data.Count > 0)
            {
                var path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\SortDetailReport.xlsx");

                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];

                ws.Cells["B1"].PutValue(userName);
                ws.Cells["B2"].PutValue(DateTime.Now);
                designer.SetDataSource("result", data);
                designer.Process();

                ws.AutoFitColumns();
                ws.PageSetup.CenterHorizontally = true;
                ws.PageSetup.FitToPagesWide = 1;
                ws.PageSetup.FitToPagesTall = 0;

                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            return stream.ToArray();
        }

    }
}