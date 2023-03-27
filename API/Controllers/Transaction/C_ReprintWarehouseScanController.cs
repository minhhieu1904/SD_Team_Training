using API._Services.Interfaces.transaction;
using API.DTOs.Transaction.ReprintWarehouseScan;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_ReprintWarehouseScanController : APIController
    {
        private readonly I_ReprintWarehouseScanService _service;
        public C_ReprintWarehouseScanController(I_ReprintWarehouseScanService service)
        {
            _service = service;
        }

        [HttpGet("GetDataExport")]
        public async Task<IActionResult> GetDataExport([FromQuery] List<ReprintWarehouseScanDto> dataReprints)
        {
            var result = await _service.GetDataExport(dataReprints);
            return Ok(result);
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] ReprintWarehouseScanParam param)
        {
            var result = await _service.GetDataPagination(param, pagination);
            return Ok(result);
        }
    }
}