using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.C_WarehouseBasicdata
{

    public class C_WarehouseBasicdata : APIController
    {
        private readonly IWarehouseBasicdataService _locationData;

        public C_WarehouseBasicdata(IWarehouseBasicdataService locationData)
        {
            _locationData = locationData;
        }

        [HttpGet("getData")]
        public async Task<ActionResult> GetMs_Location([FromQuery]PaginationParam pagination,string location, string locationName)
        {
            var data = await _locationData.LoadData(pagination, location,locationName);
            return Ok(data);
        } 
        [HttpPost("Add")]
        public async Task<ActionResult> Add([FromBody] MS_Location location)
        {
            location.Manuf = "N";
            var data = await _locationData.Add(location);
            return Ok(data);
        }
        [HttpPut("update")]
         public async Task<ActionResult> Update([FromBody] MS_Location location ){
            var data = await _locationData.Update(location);
            return Ok(data);
        }
    }
}