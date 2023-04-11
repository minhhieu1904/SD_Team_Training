using API._Services.Interfaces.Report;
using API.DTOs.Report;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_QRcodeWipReportController : ControllerBase
    {
        private readonly I_QRcodeWipReportServices _service;

        public C_QRcodeWipReportController(I_QRcodeWipReportServices service)
        {
            _service = service;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] CheckSumMissReportParam param)
        {
            var result = await _service.GetDataPagination(pagination, param);
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] CheckSumMissReportParam param)
        {
            var result = await _service.ExportExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }
    }
}