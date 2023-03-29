using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs.DepartmentDataMaintain;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;
using API.Models;

namespace API.Controllers.maintain
{
    public class C_DepartmentDataMaintainController : APIController
    {
        public readonly I_DepartmentDataMaintainServices _services;
        public C_DepartmentDataMaintainController(I_DepartmentDataMaintainServices services){
            _services = services;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] DepartmentDataMaintainParam param){
            var result = await _services.GetData(pagination, param);
            return Ok(result);
        }
        [HttpGet("GetDataOnly")]
        public async Task<ActionResult> GetDataOnly(string manuf, string parNo){
            var result = await _services.GetDataOnly(manuf, parNo);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] MsDepartment model){
            var result = await _services.Add(model);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MsDepartment model){
            var result = await _services.Update(model);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string parNo){
            var result = await _services.Delete(parNo);
            return Ok(result);
        }


    }
}