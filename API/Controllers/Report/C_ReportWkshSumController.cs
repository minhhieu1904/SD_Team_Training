using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers
{
    public class C_ReportWkshSumController : APIController
    {

        private I_Report_wksh_SumServices _service;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public C_ReportWkshSumController(I_Report_wksh_SumServices service, IWebHostEnvironment webHostEnvironment)
        {
            _service = service;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam param, [FromQuery] ReportWkshSum model)
        {
            var result = await _service.GetDataPaging(param, model);
            return Ok(result);
        }

        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var result = await _service.GetBrands();
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] ReportWkshSum model)
        {
            var data = await _service.ExportExcel(model, userName);
            return Ok(data);
        }
    }
}