using API._Services.Interfaces.Report;
using API.DTOs.Report;
using API.Helpers;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{
    public class C_StorageSumReport : APIController
    {
        private I_StorageSumReport _service;
        private IWebHostEnvironment _webHostEnvironment;

        public C_StorageSumReport(I_StorageSumReport service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, [FromQuery] Report_Storage_Sum_Param param, bool isPaging = true)
        {
            return Ok(await _service.Search(pagination, param));
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PaginationParam pagination, [FromQuery] Report_Storage_Sum_Param param)
        {
            var data = await _service.Search(pagination, param, false);
            MemoryStream stream = new MemoryStream();
            if (data.Result.Count > 0)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\Report\\StorageSumReport\\StorageSumReportTemplate.xlsx");
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
        public async Task<IActionResult> ExportDetailExcel([FromQuery] StorageExportDetailExcelParams param)
        {
            var data = await _service.ExportDetailExcel(param);
            MemoryStream stream = new MemoryStream();
            if (data.Count > 0)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\Report\\StorageSumReport\\StorageSumReportDetail.xlsx");
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