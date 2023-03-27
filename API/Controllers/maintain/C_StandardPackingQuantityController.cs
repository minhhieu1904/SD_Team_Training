using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs.StandardPackingQuantity;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.maintain
{
    public class C_StandardPackingQuantityController : APIController
    {
        public readonly I_StandardPackingQuantityServices _services;
        public C_StandardPackingQuantityController(I_StandardPackingQuantityServices services){
            _services = services;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination,[FromQuery] StandardPackingQuantityParam param){
            var result = await _services.GetData(pagination, param);
            return Ok(result);
        }
        [HttpGet("GetDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string packageNo){
            var result = await _services.GetDataOnly(manuf, packageNo);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] MsPackage model){
            var result = await _services.Add(model);
            return Ok(result);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MsPackage model){
            var result = await _services.Update(model);
            return Ok(result);
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string packageNo){
            var result = await _services.Delete(packageNo);
            return Ok(result);
        }
        
    }
}