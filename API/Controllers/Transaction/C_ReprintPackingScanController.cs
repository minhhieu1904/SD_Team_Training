using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.ReprintPackingScan;
using API.Helpers.Params.Transaction;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_ReprintPackingScanController : APIController
    {
        public readonly I_ReprintPackingScanService _service;
        public C_ReprintPackingScanController(I_ReprintPackingScanService service)
        {
            _service = service;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] ReprintPackingScanParam param)
        {
            var data = await _service.GetData(pagination, param);
            return Ok(data);
        }

        [HttpPost("getDataReprint")]
        public async Task<IActionResult> GetExportData([FromBody] List<ReprintPackingScanModel> model)
        {
            var data = await _service.GetExportData(model);
            return Ok(data);
        }
    }
}