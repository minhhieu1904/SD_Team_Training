using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.report;
using API.DTOs.report.QRCodeWIPReport;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.report
{
    public class C_QRCodeWIPReport : APIController
    {
        public readonly I_QRCodeWIPReportServices _service;    
        public C_QRCodeWIPReport(I_QRCodeWIPReportServices service){
            _service = service;
        }
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination, [FromQuery]QRCodeParam param, bool isPaging = true){
            return Ok(await _service.GetDataPagination(pagination,param, isPaging));
        }
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] QRCodeParam param)
        {
            var result = await _service.ExportExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }
    }
}