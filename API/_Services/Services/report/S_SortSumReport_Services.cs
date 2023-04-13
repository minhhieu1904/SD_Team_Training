using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.report;
using API.Data;
using API.DTOs;
using API.DTOs.report;
using API.Models;
using Aspose.Cells;
using LinqKit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.report
{
    public class S_SortSumReport_Services : I_SortSumReport_Services
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IWebHostEnvironment _environment;
        public DBContext _dbContext;
        public S_SortSumReport_Services(IRepositoryAccessor repositoryAccessor, DBContext dbContext, IWebHostEnvironment environment)
        {
            _repositoryAccessor = repositoryAccessor;
            _environment = environment;
            _dbContext = dbContext;
        }

        public async Task<PaginationUtility<SortSumReportDTO>> GetData(PaginationParam pagination ,SortSumReportParam param, bool isPaging = true)
        {
            var data = await _dbContext.Sort_Sum_Report.FromSqlRaw
             ("EXEC Report_Sort_Sum @p_date_kind, @p_date_start, @p_date_end, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
             new SqlParameter("p_date_kind", !string.IsNullOrEmpty(param.date_kind) ? param.date_kind : (object)DBNull.Value),
             new SqlParameter("p_date_start", param.date_start.HasValue ? param.date_start.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_date_end", param.date_end.HasValue ? param.date_end.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_brand", !string.IsNullOrEmpty(param.brandname) ? param.brandname : (object)DBNull.Value),
             new SqlParameter("p_code_of_customer", !string.IsNullOrEmpty(param.cusna) ? param.cusna : (object)DBNull.Value),
             new SqlParameter("p_planning_no", !string.IsNullOrEmpty(param.manno) ? param.manno : (object)DBNull.Value),
             new SqlParameter("p_mold_num", !string.IsNullOrEmpty(param.rmodel) ? param.rmodel : (object)DBNull.Value),
             new SqlParameter("p_tolcls", !string.IsNullOrEmpty(param.tolcls) ? param.tolcls : (object)DBNull.Value),
             new SqlParameter("p_purchase_no", !string.IsNullOrEmpty(param.purno) ? param.purno : (object)DBNull.Value),
             new SqlParameter("p_material_code", !string.IsNullOrEmpty(param.manno) ? param.manno : (object)DBNull.Value),
             new SqlParameter("p_kind", !string.IsNullOrEmpty(param.kind) ? param.kind : (object)DBNull.Value),
             new SqlParameter("p_etd_start", param.eta_start.HasValue ? param.eta_start.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_etd_end", param.eta_end.HasValue ? param.eta_end.Value.Date : (object)DBNull.Value),
             new SqlParameter("p_size", !string.IsNullOrEmpty(param.size) ? param.size : (object)DBNull.Value)
             ).ToListAsync();

            return PaginationUtility<SortSumReportDTO>.Create(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<List<BrandDTO>> GetBrand()
        {
            return await _repositoryAccessor.MsQrOrder.FindAll().Select(x => new BrandDTO { brandname = x.Brandname, id = x.Brandname }).Distinct().ToListAsync();
        }

        public async Task<byte[]> ExportExcel(PaginationParam pagination, SortSumReportParam param)
        {
            var data = await GetData(pagination, param, false);
            MemoryStream stream = new MemoryStream();
            if (data.Result.Count > 0)
            {
                var path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\SortSumReport.xlsx");

                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];
                designer.SetDataSource("result", data.Result);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
                ws.AutoFitColumns();
                ws.PageSetup.CenterHorizontally = true;
                ws.PageSetup.FitToPagesWide = 1;
                ws.PageSetup.FitToPagesTall = 0;
            }
            return stream.ToArray();
        }



        public async Task<byte[]> ExportDetailExcel(SortSumDetailReportParam param)
        {
            var pred = PredicateBuilder.New<MsQrLabel>(true);
            if(!string.IsNullOrWhiteSpace(param.manno)) 
                pred.And(x => x.ManNo == param.manno);
            if(!string.IsNullOrWhiteSpace(param.purno))
                pred.And(x => x.PurNo == param.purno);
            if(!string.IsNullOrWhiteSpace(param.size))
                pred.And(x => x.Size == param.size);
            var data = await _repositoryAccessor.MSQrLabel.FindAll(pred)
                        .GroupJoin(_repositoryAccessor.MsQrOrder.FindAll(), 
                            l => new { ManNo = l.ManNo,PurNo = l.PurNo,Size = l.Size },
                            s => new { ManNo = s.Manno, PurNo = s.Purno, Size = s.Size },
                            (l,s) => new { T1 = l, T2 = s }
                        ).SelectMany(x => x.T2.DefaultIfEmpty(), (x,y) => new { x.T1, T2 = y }) 
                        .GroupJoin(_repositoryAccessor.MSQrSort.FindAll(),
                            x => x.T1.QrcodeId,
                            y => y.QrcodeId,
                            (x,y) => new { x.T1, x.T2, T3 = y}
                        ).SelectMany(x => x.T3.DefaultIfEmpty(), (x,y) => new { x.T1, x.T2, T3 = y})
                        .Select(x => new SortSumReportDetail(){
                            IsScanSort = x.T3.QrcodeId != null ? "Y" : "N",
                            CrDay = x.T3.CrDay.ToString("dd/MM/yyyy"),
                            brandname = x.T2.Brandname,
                            QRCodeID = x.T1.QrcodeId,
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

            MemoryStream stream = new MemoryStream();
            if (data.Count > 0)
            {
                var path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\SortDetailReport.xlsx");
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];
                designer.SetDataSource("result", data);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
                ws.AutoFitColumns();
                ws.PageSetup.CenterHorizontally = true;
                ws.PageSetup.FitToPagesWide = 1;
                ws.PageSetup.FitToPagesTall = 0;
            }
            return stream.ToArray();
        }
    public void workbooks(){}
    }
}
