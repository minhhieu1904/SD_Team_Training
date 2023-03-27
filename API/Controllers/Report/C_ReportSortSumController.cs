using API._Services.Interfaces.report;
using API.DTOs.Report;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers
{
    public class C_ReportSortSumController : APIController
    {
        private readonly I_Report_Sort_SumServices _service;
        private readonly IWebHostEnvironment _environment;

        public C_ReportSortSumController(I_Report_Sort_SumServices service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand()
        {
            var result = await _service.GetBrands();
            return Ok(result);
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam param, [FromQuery] ReportSortSum model)
        {
            var result = await _service.GetDataPaging(param, model);
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] ReportSortSum model)
        {
            var data = await _service.ExcelExport(model, userName);
            return Ok(data);
        }

        [HttpGet("ExportExcelDetail")]
        public async Task<IActionResult> ExportExcelDetail([FromQuery] string manno, [FromQuery] string purno, [FromQuery] string size)
        {
            var data = await _service.ExcelExportDetail(manno, purno, size, userName);
            return Ok(data);
        }
    }
}


