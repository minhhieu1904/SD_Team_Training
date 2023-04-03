using System.Diagnostics.Contracts;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API._Services.Interfaces;
using API.Models;
using SD3_API.Helpers.Utilities;
using API.Helper.Params.ShiftDataMaintain;

namespace API.Controllers
{
    public class ShiftDataMaintainController: APIController
    {
        private readonly IShiftDataMaintainService _service;
        public ShiftDataMaintainController(IShiftDataMaintainService service)
        {
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery]PaginationParam pagination,[FromQuery] ShiftDataMaintainParam param) 
        { 
            var result = await _service.GetAll(pagination,param);
            return Ok(result);
        }

        [HttpGet("GetItem")]
        public async Task<IActionResult> GetItem(string manuf, string shift) 
        { 
            var result = await _service.GetItem(manuf,shift);
            return Ok(result);
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_Shift msshifts) 
        { 
            var result = await _service.Create(msshifts);
            return Ok(result);
        }
        
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MS_Shift msshifts) 
        { 
            var result = await _service.Update(msshifts);
            return Ok(result);
        }

    }
}