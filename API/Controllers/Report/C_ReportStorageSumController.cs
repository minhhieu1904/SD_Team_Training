using API._Services.Interfaces.report;
using API.DTOs.Report;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers
{
    public class C_ReportStorageSumController : APIController
    {
        private readonly I_Report_StorageSumService _service;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public C_ReportStorageSumController(I_Report_StorageSumService service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var res = await _service.GetBrand();
            return Ok(res);
        }

        [HttpGet("GetDataPaing")]
        public async Task<IActionResult> GetDataPaing([FromQuery] PaginationParam param, [FromQuery] ReportStorageSum model)
        {
            var result = await _service.GetDataPaing(param, model);
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] ReportStorageSum model)
        {

            var data = await _service.ExportExcel(model, userName);
            return Ok(data);
        }

        [HttpGet("ExportExcelDetail")]
        public async Task<IActionResult> ExportExcelDetail(string manno, string purno, string size)
        {

            var data = await _service.ExportExcelDetail(userName, manno, purno, size);
            return Ok(data);
        }
    }
}