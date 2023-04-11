
using API._Services.Interfaces.Report;
using API.DTOs.Report;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;

using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{

    public class C_Report_Storage_SumController : APIController
    {
        private readonly IReport_Storage_Sumservice _reportService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_Report_Storage_SumController(IReport_Storage_Sumservice reportService, IWebHostEnvironment webHostEnvironment)
        {
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] Storage_sumDTO param, bool isPaging = true)
        {
            var result = await _reportService.GetData(pagination,param,isPaging);
            return Ok(result);
        }
        [HttpGet("Export")]
        public async Task<ActionResult> ExportExcel ([FromQuery] PaginationParam pagination, [FromQuery] Storage_sumDTO param)
        {
            var data = await _reportService.GetData(pagination,param,false);
            MemoryStream stream = new MemoryStream();
            if(data.Result.Any())
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources\\Template\\Report\\4.3Report_Storage_Sum\\Report_Storage_Sum.xlsx");
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];
                ws.Cells[1,1].Value = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:sstt");
                designer.SetDataSource("result",data.Result);
                designer.Process();
                designer.Workbook.Save(stream,SaveFormat.Xlsx);
            }
            byte[] result = stream.ToArray();
            return File(result,"application/xlsx",$"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");

        }
         [HttpGet("ExportExcelDetail")]
        public async Task<ActionResult> ExportExcelDetail([FromQuery]PaginationParam pagination, [FromQuery] StorageSumDeltailDTOParam param)
        {
            
            var result = await _reportService.ExportExcelDetails(param);
            return File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");
        }
        [HttpGet("getBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var result = await _reportService.GetBrand();
            return Ok(result);
        }
    }
}