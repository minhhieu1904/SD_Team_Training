using API._Services.Interfaces;
using API.DTOs.WarehouseBasicData;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Maintain
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_WarehouseBasicDataController : APIController
    {
        private readonly I_WarehouseBasicDataServices _services;

        public C_WarehouseBasicDataController(I_WarehouseBasicDataServices services)
        {
            _services = services;
        }

        [HttpGet("GetData")]
        public async Task<ActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] WarehouseBasicDataParam param)
        {
            var result = await _services.GetData(pagination, param);
            return Ok(result);
        }

        [HttpGet("GetDataOnly")]
        public async Task<ActionResult> GetDataOnly(string manuf, string StoreH)
        {
            var result = await _services.GetDataOnly(manuf, StoreH);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] MsLocation model)
        {
            var result = await _services.Add(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] MsLocation model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(string StoreH)
        {
            var result = await _services.Delete(StoreH);
            return Ok(result);
        }
    }
}