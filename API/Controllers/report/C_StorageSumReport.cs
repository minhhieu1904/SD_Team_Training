using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.report;
using API.DTOs.report;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.report
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_StorageSumReport : APIController
    {
        public readonly I_StorageSumReport_Services _service;
        public C_StorageSumReport(I_StorageSumReport_Services service){
            _service = service;
        }
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] StorageSumReportParam param, bool isPaging = true){
            return Ok(await _service.GetData(pagination,param,isPaging));
        }
        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand(){
            return Ok(await _service.GetBrand());
        }
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery]PaginationParam pagination, [FromQuery] StorageSumReportParam param){
            var result = await _service.ExportExcel(pagination, param);
            return await Task.FromResult(File(result, "application/xlsx", $"StorageSumReport.xlsx"));
        }
        [HttpGet("ExportDetailExcel")]
        public async Task<IActionResult> ExportDetailExcel([FromQuery] StorageSumReportDetailParam param){
            var result = await _service.ExportDetailExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"StorageSumDetailReport.xlsx"));
        }
    }
}