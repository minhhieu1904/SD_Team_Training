using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.report;
using API.DTOs.report.SortSumReport;
using API.Models;
using API.Models.report;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.report
{
    public class C_SortSumReport : APIController
    {
        private I_SortSumReport _services;
        private readonly IWebHostEnvironment _webHostEnvironment; 
        public C_SortSumReport ( I_SortSumReport services,IWebHostEnvironment webHostEnvironment )
        {
            _services = services;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] SortSumReport param,bool isPaging = true)
        {
            var result = await _services.GetData(pagination,param,isPaging);
            return Ok(result);
        }
        [HttpGet("getBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var result = await _services.GetBrand();
            return Ok(result);
        }
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery]PaginationParam pagination ,[FromQuery] SortSumReport param)
        {

            var data = await _services.GetData(pagination,param,false);
            MemoryStream stream = new MemoryStream();
            if(data.Result.Count > 0){
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\SortSumReport.xlsx");
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
            return File(result," application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy-HH_mm_SS")}.xlsx");
        }
        [HttpGet("ExportDetailExcel")]
        public async Task<IActionResult> ExportDetailExcel([FromQuery]SortSumDetailReportParam param)
        {

            var data = await _services.GetDataDetail(param);
            MemoryStream stream = new MemoryStream();
            if(data.Count > 0){
                var path = Path.Combine(_webHostEnvironment.ContentRootPath,
                "Resources\\Template\\SortDetailReport.xlsx");
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
            return File(result," application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy-HH_mm_SS")}.xlsx");
        }

    }
}