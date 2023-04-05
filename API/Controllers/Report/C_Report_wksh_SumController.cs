using System;

using API._Services.Interfaces;
using API._Services.Interfaces.Report;
using API.DTOs.Report;
using API.Models;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{
 
    public class C_Report_wksh_SumController : APIController
    { 
        private readonly IReport_wksh_SumService _report;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_Report_wksh_SumController(IReport_wksh_SumService report, IWebHostEnvironment webHostEnvironment)
        {
            _report = report;
            _webHostEnvironment = webHostEnvironment;
        }
         [HttpGet("getBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var result = await _report.GetBrand();
            return Ok(result);
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] Report_wksh_SumResult_Param param,bool isPaging = true)
        {
            var result = await _report.GetData(pagination,param,isPaging);
            return Ok(result);
        }
        [HttpGet("Export")]
        public async Task<ActionResult> ExportExcel([FromQuery]PaginationParam pagination, [FromQuery] Report_wksh_SumResult_Param param)
        {
            var data = await _report.GetData(pagination,param,false);
            MemoryStream stream = new MemoryStream();
            if (data.Result.Any())
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources\\Template\\Report\\4.1Report_wksh_Sum\\Report_wksh_Sum.xlsx");
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];
                designer.SetDataSource("result", data.Result);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
                
            }
            byte[] result = stream.ToArray();
            return File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");
        }

    }
}