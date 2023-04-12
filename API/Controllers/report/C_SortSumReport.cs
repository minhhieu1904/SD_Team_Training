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
    public class C_SortSumReport : APIController
    {
        public readonly I_SortSumReport_Services _service;
        public C_SortSumReport(I_SortSumReport_Services service){
            _service = service;
        }
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination, [FromQuery]SortSumReportParam param, bool isPaging = true){
            return Ok(await _service.GetData(pagination, param, isPaging));
        }
        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery]PaginationParam pagination, [FromQuery] SortSumReportParam param){
            var result = await _service.ExportExcel(pagination, param);
            return await Task.FromResult(File(result, "application/xlsx", $"SortSumReport.xlsx"));
        }
        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand(){
            return Ok(await _service.GetBrand());
        }

        [HttpGet("ExportDetailExcel")]
        public async Task<IActionResult> ExportDetailExcel([FromQuery] SortSumDetailReportParam param){
            var result = await _service.ExportDetailExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"SortSumDetailReport.xlsx"));
        }
        
    }   

}