using API._Services.Interfaces.Report;
using API.DTOs.Report;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;
namespace API.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_SortSumReportController : ControllerBase
    {
        private readonly I_SortSumReportServices _service;

        public C_SortSumReportController(I_SortSumReportServices service)
        {
            _service = service;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetDataPagination([FromQuery]PaginationParam pagination,[FromQuery] SortSumReportParam param)
        {
            var data = await this._service.GetDataPagination(pagination, param);
            return Ok(data);
        }

        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand(){
            var result = await _service.GetBrand();
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] SortSumReportParam param){
            var result = await _service.ExportExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }

        [HttpGet("ExportDetailExcel")]
        public async Task<IActionResult> ExportDetailExcel([FromQuery] SortSumDetailReportParam param){
            var result = await _service.ExportDetailExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }
        
    }
}