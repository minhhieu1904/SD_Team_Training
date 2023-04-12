using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.Report;
using API.DTOs.Report;
using API.Models;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{

    public class C_Report_QRCODE_WIPController : APIController
    {
        private readonly IReport_QRCODE_WIPservices _reportService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_Report_QRCODE_WIPController(IReport_QRCODE_WIPservices reportService, IWebHostEnvironment webHostEnvironment)
        {
            _reportService = reportService;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ReportQRCODEWIPParam  param)
        {
            var result = await _reportService.GetDataPagination(pagination, param);
            return Ok(result);
        }
        [HttpGet("Export")]
        public async Task<ActionResult> ExportExcel ( [FromQuery] ReportQRCODEWIPParam param)
        {
            var result = await _reportService.ExportExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }
    }
}