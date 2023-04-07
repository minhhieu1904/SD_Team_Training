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
    public class DepartmentDataMaintenanceController : APIController
    {
        private readonly IDepartmentDataMaintenanceService _service;
        public DepartmentDataMaintenanceController(IDepartmentDataMaintenanceService service)
        {
            _service = service;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParam pagination, [FromQuery] DepartmentDataMaintenanceParam param)
        {
            var result = await _service.GetAll(pagination, param);
            return Ok(result);
        }
        
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, string text)
        {
            var result = await _service.Search(pagination, text);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_Department model)
        {
            var result = await _service.Create(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MS_Department model)
        {
            var result = await _service.Update(model);

            return Ok(result);
        }
    }
}