
using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class C_ShiftDataMaintain : APIController
    {
        private readonly I_Shift_Data_Maintain _service;// nó sẽ dùng cá Interface này

        public C_ShiftDataMaintain(I_Shift_Data_Maintain service)
        {
            _service = service;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_Shift_DTO dto)
        {
            var data = await _service.Create(dto);
            return Ok(data);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, [FromQuery] MS_Shift_DTO dto)
        {
            var data = await _service.Search(pagination, dto);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(MS_Shift_DTO dto)
        {
            var data = await _service.Update(dto);
            return Ok(data);
        }

        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(MS_Shift_DTO dto)
        {
            var data = await _service.GetDetail(dto);
            return Ok(data);
        }
    }
}