using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs.ShiltDataMaintain;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.maintain
{
  
    public class C_ShiftDataMaintainController : APIController
    {
        private readonly I_ShiftDataMaintain _services;

        public C_ShiftDataMaintainController(I_ShiftDataMaintain services)
        {
            _services = services;
        }

        [HttpGet("get-data")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] ShiftDataMaintainParam param)
        {
            var result = await _services.GetData(pagination,param);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]MS_Shift model)
        {
            var result = await _services.Addnew(model);
            return Ok(result);
        }

        [HttpGet("getDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string shift)
        {
            var result = await _services.GetDataOnly(manuf, shift);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]MS_Shift model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody]MS_Shift model)
        {
            var result = await _services.Delete(model);
            return Ok(result);
        }

    }
}