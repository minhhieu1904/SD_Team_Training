using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.maintain
{
    public class C_StandardPackingQuantity  : APIController
    {
        private readonly I_StandardPackingQuantity _services;

        public C_StandardPackingQuantity(I_StandardPackingQuantity services)
        {
            _services = services;
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] StandardPackingQuantityParam param)
        {
            var result = await _services.GetData(pagination,param);
            return Ok(result);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]StandardPackingQuantityAddParam model)
        {
            var result = await _services.Addnew(model);
            return Ok(result);
        }

        [HttpGet("getDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string PackageNo)
        {
            var result = await _services.GetDataOnly(manuf, PackageNo);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]StandardPackingQuantityAddParam model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }
    }
}