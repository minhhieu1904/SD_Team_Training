using System.Globalization;
using API._Services.Interfaces.Report;
using API.Helpers.Params.Report;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{
    public class C_QRCodeWIPReport : APIController
    {
        private readonly I_QRCodeWIPReport _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_QRCodeWIPReport(I_QRCodeWIPReport service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, [FromQuery] QRcodeWIPReportParam param)
        {
            return Ok(await _service.Search(pagination, param));
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PaginationParam pagination, [FromQuery] QRcodeWIPReportParam param)
        {
            var data = await _service.Search(pagination, param, false);
            MemoryStream stream = new MemoryStream();
            if (data.Headers.Count > 0 || data.Details.Result.Count > 0)
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\Report\\QRCodeWIPReport\\QRCodeWIPReportTemp.xlsx");
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet wsHeader = designer.Workbook.Worksheets[0];
                Worksheet wsDetail = designer.Workbook.Worksheets[1];
                wsHeader.Cells["I2"].PutValue("統計時間: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                foreach (var dr in data.Details.Result)
                {
                    dr.yymmdd = DateTime.ParseExact(Convert.ToString(dr.yymmdd), "yyMMdd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                }
                designer.SetDataSource("headers", data.Headers);
                designer.SetDataSource("details", data.Details.Result);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            byte[] result = stream.ToArray();
            return File(result, " application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy-HH_mm_SS")}.xlsx");
        }
    }
}