using API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.Helper.Params;
using API.DTOs.Role;


namespace API.Controllers
{
    public class AuthorizationSettingController : APIController
    {
        private readonly IAuthorizationSettingService _service;
        public AuthorizationSettingController (IAuthorizationSettingService service)
        {   
            _service = service;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] SDCores.PaginationParam pagination, [FromQuery] AuthorizationSettingParam param)
        {
            var result = await _service.GetAll(pagination, param);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update ([FromBody] UserFormParam model)
        {
            var result = await _service.Update(model);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create ([FromBody] UserFormParam model)
        {
            var result = await _service.Create(model);
            return Ok(result);
        }

        [HttpGet("GetAllRoleByAccount")]
        public async Task<IActionResult> GetAllRoleByAccount(string account)
        {
            var result = await _service.GetAllRoleByAccount(account);
            return Ok(result);
        }

        [HttpPut("UpdateRoleByAccount")]
        public async Task<IActionResult> UpdateRoleByAccount([FromBody]UserRoleDTO model)
        {
            var result = await _service.UpdateRoleByAccount(model);
            return Ok(result);
        }
    }
}