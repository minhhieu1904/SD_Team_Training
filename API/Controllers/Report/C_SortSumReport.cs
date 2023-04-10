using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.Report;
using API.DTOs.Report;
using API.Helpers.Params.SortSumReport;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{
    public class C_SortSumReport : APIController
    {
        private I_SortSumReport _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_SortSumReport(I_SortSumReport service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, [FromQuery] SearchSortSumReportParams param)
        {
            return Ok(await _service.Search(pagination, param));
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PaginationParam pagination, [FromQuery] SearchSortSumReportParams param)
        {
            var data = await _service.Search(pagination, param, false);
            MemoryStream stream = new MemoryStream();
            if (data.Result.Count > 0)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\Report\\SortSumReport\\SortSumReportTemplate.xlsx");
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
            byte[] result = stream.ToArray();
            return File(result, " application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy-HH_mm_SS")}.xlsx");
        }

        [HttpGet("ExportDetailExcel")]
        public async Task<IActionResult> ExportDetailExcel([FromQuery] ExportDetailExcelParams param)
        {
            var data = await _service.ExportDetailExcel(param);
            MemoryStream stream = new MemoryStream();
            if (data.Count > 0)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\Report\\SortSumReport\\4.2SortDetailReport.xlsx");
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
            byte[] result = stream.ToArray();
            return File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy-HH_mm_SS")}.xlsx");
        }
    }
}