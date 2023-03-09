
using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class Shift_Data_MaintainController : APIController
    {
        private readonly I_Shift_Data_Maintain _service;// nó sẽ dùng cá Interface này

        public Shift_Data_MaintainController(I_Shift_Data_Maintain service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromQuery]MS_Shift_DTO dto) => Ok(await _service.Create(dto));

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, [FromQuery] MS_Shift_DTO dto) => Ok(await _service.Search(pagination ,dto));

        [HttpPut("Update")]
        public async Task<IActionResult> Update(MS_Shift_DTO dto) {
        var data=  await _service.Update(dto);
        return Ok(data);
        }


        //async await này trên trường thầy Hiếu có dạy đồng bộ và bất đồng bộ dó a, có gì a gg thêm nhé Oke anh có clip có gì xem lại
        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(MS_Shift_DTO dto) {
        var data=  await _service.GetDetail(dto);
        return Ok(data);
        }
    }
}