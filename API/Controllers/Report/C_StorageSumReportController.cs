using API._Services.Interfaces.Report;
using API.DTOs.Report;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_StorageSumReportController : APIController
    {
        private readonly I_StorageSumReportServices _service;

        public C_StorageSumReportController(I_StorageSumReportServices service)
        {
            _service = service;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetDataPagination([FromQuery]PaginationParam pagination,[FromQuery] StorageSumReportParam param)
        {
            var result = await _service.GetDataPagination(pagination, param);
            return Ok(result);
        }
        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrands()
        {
            var result = await _service.GetBrands();
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] StorageSumReportParam param)
        {
            var result = await _service.ExportExcel(param, "");
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }

        [HttpGet("ExportDetailExcel")]
        public async Task<IActionResult> ExportDetailExcel([FromQuery] StorageSumDetailReportParam param)
        {
            var result = await _service.ExportDetailExcel(param, "");
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }

    }
}