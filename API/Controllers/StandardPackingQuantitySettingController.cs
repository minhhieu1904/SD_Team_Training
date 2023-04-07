using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class StandardPackingQuantitySettingController : APIController
    {
        private readonly IStandardPackingQuantitySettingService _service;

        public StandardPackingQuantitySettingController(IStandardPackingQuantitySettingService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParam pagination, [FromQuery] StandardPackingQuantitySettingParam param)
        {
            var result = await _service.GetAll(pagination, param);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_Package model)
        {
            var result = await _service.Create(model);
            return Ok(result);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search ([FromQuery] PaginationParam pagination, string text)
        {
            var result = await _service.Search(pagination, text);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MS_Package model)
        {
            var result = await _service.Update(model);
            return Ok(result);
        }
    }
}