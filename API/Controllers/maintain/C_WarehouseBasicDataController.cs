
using API._Services.Interfaces;
using API.DTOs.WarehouseBasicData;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.maintain
{
    public class C_WarehouseBasicDataController : APIController
    {
        private readonly I_WarehouseBasicData _services;

        public C_WarehouseBasicDataController(I_WarehouseBasicData services)
        {
            _services = services;
        }
        [HttpGet("get-data")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] WarehouseBasicDataParam param)
        {
            var result = await _services.GetData(pagination,param);
            return Ok(result);
        }
        

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]MS_Location model)
        {
            var result = await _services.Addnew(model);
            return Ok(result);
        }

        [HttpGet("getDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string StoreH)
        {
            var result = await _services.GetDataOnly(manuf, StoreH);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]MS_Location model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody]MS_Location model)
        {
            var result = await _services.Delete(model);
            return Ok(result);
        }
    }
}