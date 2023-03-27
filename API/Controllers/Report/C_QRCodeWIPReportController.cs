
using API._Services.Interfaces.report;
using API.DTOs.Report;
using API.Helpers.Params.Report;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers
{
    public class C_QRCodeWIPReportController : APIController
    {
        private readonly I_CheckSumMissServices _service;

        public C_QRCodeWIPReportController(I_CheckSumMissServices service)
        {
            _service = service;
            ;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetDataPaing([FromQuery] PaginationParam pagination, [FromQuery] QRcodeWIPReportParam param)
        {
            var res = await _service.GetCheckSumMissData(pagination, param);
            return Ok(res);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] PaginationParam pagination, QRcodeWIPReportParam param)
        {
            var result = await _service.ExportExcel(pagination, param);
            return Ok(result);
        }










































































































































    }
}