using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.Report;
using API.DTOs.Report;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{

    public class C_Report_Sort_SumController : APIController
    {
        private readonly IReport_Sort_Sumservice _reportService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_Report_Sort_SumController(IReport_Sort_Sumservice reportService, IWebHostEnvironment webHostEnvironment)
        {
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
        }
         [HttpGet("getBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var result = await _reportService.GetBrand();
            return Ok(result);
        }
        [HttpGet("getData")]
          public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] SortSumDTO param,bool isPaging = true)
        {
            var result = await _reportService.GetData(pagination,param,isPaging);
            return Ok(result);
        }
        [HttpGet("Export")]
         public async Task<ActionResult> ExportExcel([FromQuery]PaginationParam pagination, [FromQuery] SortSumDTO param)
        {
            var data = await _reportService.GetData(pagination,param,false);
            MemoryStream stream = new MemoryStream();
            if (data.Result.Any())
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources\\Template\\Report\\4.2Report_Sort_Sum\\Report_Sort_Sums.xlsx");
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws = designer.Workbook.Worksheets[0];
                ws.Cells[1, 1].Value = DateTime.Now.ToString("yyyy/MM/dd/HH:mm:sstt");
                designer.SetDataSource("result", data.Result);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            byte[] result = stream.ToArray();
            return File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");
        }

        [HttpGet("ExportExcelDeltail")]
        public async Task<ActionResult> ExportExcelDeltail([FromQuery]PaginationParam pagination, [FromQuery] SortSumDeltailDTOParam param)
        {
            
            var result = await _reportService.ExportExcelDetails(param);
            return File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx");
        }

    }
}