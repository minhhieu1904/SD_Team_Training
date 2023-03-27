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
    public class MSDepartmentController: APIController
    {
        private readonly IMSDepartmentServices _services;
        public MSDepartmentController(IMSDepartmentServices services)
        {
            _services = services;
        }

        [HttpGet("GetDataPaging")]
        public async Task <IActionResult> GetDataPaing([FromQuery] PaginationParam param, string parNo, string parName) 
        {
            var result = await _services.GetPagingData(param, parNo, parName);
            return Ok(result);
        }

        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew([FromBody] MS_Department model)
        {
            var result = await _services.AddNew(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateDepartment([FromBody] MS_Department model) 
        {
            var result = await _services.Update(model);
            return Ok(result);
        }

        [HttpGet("get-item")]
        public async Task<IActionResult> GetItem(string manuf, string parNo)
        {
            var result = await _services.GetItem(manuf, parNo);
            return Ok(result);
        }
    }
}