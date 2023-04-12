using API._Services.Interfaces;
using API.DTOs.Maintain.ShiftDataMaintain;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Maintain
{

    public class C_ShiftDataMaintainController : APIController
    {
        private readonly I_ShiftDataMaintainServices _services;

        public C_ShiftDataMaintainController(I_ShiftDataMaintainServices services)
        {
            _services = services;
        }

        [HttpGet("get-data")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] ShiftDataMaintainParam param)
        {
            var result = await _services.GetData(pagination, param);
            return Ok(result);
        }

        [HttpGet("GetDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string shift)
        {
            var result = await _services.GetDataOnly(manuf, shift);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] MsShift model)
        {
            var result = await _services.Addnew(model);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] MsShift model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }

    }
}