using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Maintain
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_StandardPackingQuantityController : ControllerBase
    {
        private readonly I_StandardPackingQuantityServices _services;

        public C_StandardPackingQuantityController(I_StandardPackingQuantityServices services)
        {
            _services = services;
        }

        [HttpGet("GetData")]
        public async Task<ActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] StandardPackingQuantityParam param)
        {
            var result = await _services.GetData(pagination, param);
            return Ok(result);
        }

        [HttpGet("GetDataOnly")]
        public async Task<ActionResult> GetDataOnly(string manuf, string packageNo)
        {
            var result = await _services.GetDataOnly(manuf, packageNo);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] StandardPackingQuantityParam model)
        {
            var result = await _services.Add(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] StandardPackingQuantityParam model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(string packageNo)
        {
            var result = await _services.Delete(packageNo);
            return Ok(result);
        }
    }
}