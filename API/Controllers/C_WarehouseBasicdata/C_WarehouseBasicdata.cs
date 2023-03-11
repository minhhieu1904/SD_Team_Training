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

        [HttpGet("getdata")]
        public async Task<ActionResult> GetMs_Location([FromQuery]PaginationParam pagination,string StoreH, string locationName)
        {
            var data = await _locationData.LoadData(pagination, StoreH,locationName);
            return Ok(data);
        } 
        [HttpPost("add")]
        public async Task<ActionResult> Add([FromBody] MS_Location StoreH)
        {
            StoreH.Manuf = "N";
            var data = await _locationData.Add(StoreH);
            return Ok(data);
        }
        [HttpPut("upDate")]
         public async Task<ActionResult> Update([FromBody] MS_Location StoreH ){
            var data = await _locationData.Update(StoreH);
            return Ok(data);
        }
    }
}