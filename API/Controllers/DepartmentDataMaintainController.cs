using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentDataMaintainController : Controller
    {
        private readonly I_Department_Data_Maintain _service;

        public DepartmentDataMaintainController(I_Department_Data_Maintain service)
        {
               _service = service;
        }

         [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_Department_DTO dto){
            var data = await _service.Create(dto);
            return Ok(data);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, [FromQuery] MS_Department_DTO dto){
            var data = await _service.Search(pagination, dto);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(MS_Department_DTO dto){
            var data = await _service.Update(dto);
            return Ok(data);
        }

        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(MS_Department_DTO dto){
            var data = await _service.GetDetail(dto);
            return Ok(data);
        }

      
    }
}