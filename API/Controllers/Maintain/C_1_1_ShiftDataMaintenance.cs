using API._Services.Interfaces.Maintain;
using API.DTOs.Maintain;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Maintain
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_1_1_ShiftDataMaintenance : APIController
    {
        private readonly I_1_1_ShiftDataMaintenance _service;
        private readonly IConfiguration _configuration;

        public C_1_1_ShiftDataMaintenance(I_1_1_ShiftDataMaintenance service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        //Lấy dữ liệu dùng [HttpGet] với Route là "GetData"
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] ShiftDataMaintenanceParam param) //Get dùng [FromQuery]
        {
            //Gọi phương thức xử lý từ service
            return Ok(await _service.GetDataPagination(pagination, param));
        }

        //Thêm dữ liệu dùng [HttpPost] với Route là "AddNew"
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew([FromBody] ShiftDataMaintenanceDto data) //Post, Put dùng [FromBody]
        {
            //Gọi phương thức xử lý từ service
            return Ok(await _service.AddNew(data));
        }

        //Chỉnh sửa dữ liệu dùng [HttpPut] với Route là "Edit"
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(ShiftDataMaintenanceDto data)
        {
            //Gọi phương thức xử lý từ service
            return Ok(await _service.Edit(data));
        }
    }
}