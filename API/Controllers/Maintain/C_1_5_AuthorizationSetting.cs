using API._Services.Interfaces.Maintain;
using API.Dtos.Maintain.AuthorizationSetting;
using API.DTOs.Maintain;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Maintain
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_1_5_AuthorizationSetting : APIController
    {
        private readonly I_1_5_AuthorizationSetting _service;
        private readonly IConfiguration _configuration;

        public C_1_5_AuthorizationSetting(I_1_5_AuthorizationSetting service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }

        //Lấy dữ liệu dùng [HttpGet] với Route là "GetData"
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] AuthorizationParam param) //Get dùng [FromQuery]
        {
            //Gọi phương thức xử lý từ service
            return Ok(await _service.GetDataPagination(pagination, param));
        }

        //Lấy dữ liệu dùng [HttpGet] với Route là "GetRoleUser"
        [HttpGet("GetRoleUser")]
        public async Task<IActionResult> GetRoleUser(string account)
        {
            //Gọi phương thức xử lý từ service
            return Ok(await _service.GetRoleUser(account));
        }

        //Thêm dữ liệu dùng [HttpPost] với Route là "AddNew"
        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew([FromBody] AuthorizationDto data) //Post, Put dùng [FromBody]
        {
            //Gọi phương thức xử lý từ service
            data.update_by = userName;
            data.update_time = DateTime.Now;
            return Ok(await _service.AddNew(data));
        }

        [HttpPost("EditRoleUser")]
        public async Task<IActionResult> EditRoleUser(List<RolesUserStatus> authors)
        {
            var data = await _service.EditRoleUser(authors, userName);
            return Ok(data);
        }

        //Chỉnh sửa dữ liệu dùng [HttpPut] với Route là "Edit"
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(AuthorizationDto data)
        {
            //Gọi phương thức xử lý từ service
            data.update_by = userName;
            data.update_time = DateTime.Now;
            return Ok(await _service.Edit(data));
        }
    }
}