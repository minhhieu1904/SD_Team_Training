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
    [ApiController]
    [Route("api/[controller]")]
    public class C_DepartmentDataMaintenanceController : APIController
    {
        private readonly I_DepartmentDataMaintenance _services;

        public C_DepartmentDataMaintenanceController(I_DepartmentDataMaintenance services)
        {
            _services = services;
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] DepartmentDataMaintenanceParam param)
        {
            var result = await _services.GetData(pagination,param);
            return Ok(result);
        }
        

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]MS_Department model)
        {
            var result = await _services.Addnew(model);
            return Ok(result);
        }

        [HttpGet("getDataOnly")]
        public async Task<IActionResult> GetDataOnly(string manuf, string parNo)
        {
            var result = await _services.GetDataOnly(manuf, parNo);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody]MS_Department model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }
        
    }
}