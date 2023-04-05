using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.Data;
using API.DTOs;
using Aspose.Cells;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Report
{
    public class S_SearchForPackingScanServices : I_SearchForPackingScanServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        private readonly IWebHostEnvironment _environment;

        public DBContext _dbContext;

        public S_SearchForPackingScanServices(IRepositoryAccessor repositoryAccessor, DBContext dbContext, IWebHostEnvironment environment)
        {
            _repositoryAccessor = repositoryAccessor;
            _dbContext = dbContext;
            _environment = environment;
        }

        public async Task<byte[]> ExportExcel(SearchForPackingScanParam param)
        {
            var data = await GetData(param);

            MemoryStream stream = new MemoryStream();
            if (data.Count > 0)
            {
                var path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\Development_Orders.xlsx");

                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];

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

        public async Task<List<BrandDTO>> GetBrand()
        {
            return await _repositoryAccessor.MS_QrOrder.FindAll()
            .Select(x => new BrandDTO { brandname = x.Brandname, id = x.Brandname }).Distinct().ToListAsync();
        }

        public async Task<List<SearchForPackingScanDTO>> GetData(SearchForPackingScanParam param)
        {
            return await _dbContext.SearchForPackingScans.FromSqlRaw
            ("EXEC Report_wksh_Sum @p_mdat_start, @p_mdat_end, @p_close_status, @p_brand, @p_code_of_customer, @p_planning_no, @p_mold_num, @p_tolcls, @p_purchase_no, @p_material_code, @p_kind, @p_etd_start, @p_etd_end, @p_size",
            new SqlParameter("p_mdat_start", param.mdat_start.HasValue ? param.mdat_start.Value.Date : (object)DBNull.Value),
            new SqlParameter("p_mdat_end", param.mdat_end.HasValue ? param.mdat_end.Value.Date : (object)DBNull.Value),
            new SqlParameter("p_close_status", !string.IsNullOrEmpty(param.close_status) ? param.close_status : (object)DBNull.Value),
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
        }

        public async Task<PaginationUtility<SearchForPackingScanDTO>> GetDataPagination(PaginationParam pagination, SearchForPackingScanParam param)
        {
            var data = await GetData(param);
            return PaginationUtility<SearchForPackingScanDTO>.Create(data, pagination.PageNumber, pagination.PageSize);
        }


    }
}