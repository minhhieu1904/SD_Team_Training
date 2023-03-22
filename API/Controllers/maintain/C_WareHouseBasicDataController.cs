using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs.WareHouseBasicData;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.maintain
{
    public class C_WareHouseBasicDataController : APIController
    {
        private readonly I_WareHouseBasicDataServices _services;
        public C_WareHouseBasicDataController(I_WareHouseBasicDataServices services){
            _services = services;
        }
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] WareHouseBasicDataParam param){
            var result = await _services.GetData(pagination, param);
            return Ok(result);
        }
        [HttpGet("GetDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string storeH){
            var result = await _services.GetDataOnly(manuf, storeH);
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] MsLocation model){
            var result = await _services.Add(model);
            return Ok(result);
        }
         [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MsLocation model){
            var result = await _services.Update(model);
            return Ok(result);
        }
         [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string storeH){
            var result = await _services.Delete(storeH);
            return Ok(result);
        }
    }
}