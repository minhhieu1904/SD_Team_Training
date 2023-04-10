using System.Net;
using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class Packing_QuantityController : APIController
    {
        private readonly I_Packing_Quantity_Setting _service;

        public Packing_QuantityController(I_Packing_Quantity_Setting service)
        {
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_Package_DTO dto){
            var data = await _service.Create(dto);
            return Ok(data);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination, [FromQuery] MS_Package_DTO dto){
            var data = await _service.Search(pagination, dto);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(MS_Package_DTO dto) {
            var data = await _service.Update(dto);
            return Ok(data);
        }

        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(MS_Package_DTO dto) {
            var data = await _service.GetDetail(dto);
            return Ok(data);
        }
    }
}