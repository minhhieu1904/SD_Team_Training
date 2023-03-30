
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.maintain
{
    public class C_WarehouseBasicDataController : APIController
    {
        private I_WarehouseBasicData _services;

        public C_WarehouseBasicDataController ( I_WarehouseBasicData services)
        {
            _services = services;
        }

        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] WarehouseBasicData param)
        {
            var result = await _services.getData(pagination,param);
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]MS_Location model)
        {
            var result = await _services.addNew(model);
            return Ok(result);
        }
        [HttpGet("getDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string StoreH)
        {
            var result = await _services.getDataOnly(manuf, StoreH);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]MS_Location model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }
        
    }
}