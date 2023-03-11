using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class MSLocationController : APIController
    {
        private readonly IMSLocationServices _services;

        public MSLocationController(IMSLocationServices services)
        {
            _services = services;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData()
        {
            var result = await _services.GetAllData();
            return Ok(result);
        }

        [HttpPost("AddNew")]
        public async Task<IActionResult> GetData([FromBody] MS_Location model)
        {
            var result = await _services.AddNew(model);
            return Ok(result);
        }

        [HttpPut("EditLocation")]
        public async Task<IActionResult> UpdateLocatin([FromBody] MS_Location model)
        {
            var result = await _services.UpdateLocation(model);
            return Ok(result);
        }

        [HttpGet("GetDataPaging")]
        public async Task<IActionResult> GetDataPaging([FromQuery] PaginationParam param, string storeH, string locationName)
        {
            var result = await _services.GetDataPagination(param, storeH, locationName);
            return Ok(result);
        }

        [HttpGet("GetItem")]
        public async Task<IActionResult> GetItem(string manuf, string storeH)
        {
            var result = await _services.GetItem(manuf, storeH);
            return Ok(result);
        }
    }
}