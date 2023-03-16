using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class Warehouse_Basic_Data_MaintenanceController : APIController
    {
        private readonly I_Warehouse_Basic_Data_Maintenance _service;

        public Warehouse_Basic_Data_MaintenanceController(I_Warehouse_Basic_Data_Maintenance service){
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_Location_DTO dto){
            var data = await _service.Create(dto);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Upadte(MS_Location_DTO dto){
            var data = await _service.Update(dto);
            return Ok(data);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] PaginationParam pagination,[FromQuery] MS_Location_DTO dto){
            var data = await _service.Search(pagination, dto);
            return Ok(data);
        }

        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(MS_Location_DTO dto){
            var data = await _service.Update(dto);
            return Ok(data);
        }
    }
}