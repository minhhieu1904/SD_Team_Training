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

namespace API.Controllers
{
    [Route("[controller]")]
    public class MSLocationController : Controller
    {
        private readonly IMSLocationServices _services;
        public MSLocationController(IMSLocationServices services) {
            _services = services;
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam param, string storeH, string locationName) {
            var result = await _services.GetDataPaging(param, storeH, locationName);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddNewWarehouse([FromBody] MS_Location model) {
            var result = await _services.AddNew(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateWarehouse([FromBody] MS_Location model) {
            var result = await _services.UpdateWarehouse(model);
            return Ok(result);
        }

        [HttpGet("GetItem")]
        public async Task<IActionResult> GetItem(string manuf, string storeH ) {
            var result = await _services.GetItem(manuf, storeH);
            return Ok(result);
        }
    }
}