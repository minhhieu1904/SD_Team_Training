using API._Services.Interfaces;
using API.DTOs.AuthorizationSetting;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Maintain
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_AuthorizationController : ControllerBase
    {
        private readonly I_AuthorizationServices _service;

        public C_AuthorizationController(I_AuthorizationServices service)
        {
            _service = service;
        }

        [HttpGet("GetData")]
        public async Task<ActionResult> GetData([FromQuery] PaginationParam pagination, string account)
        {
            var result = await _service.GetData(pagination, account);
            return Ok(result);
        }

        [HttpGet("GetDataOnly")]
        public async Task<IActionResult> GetDataOnly(string account)
        {
            var result = await _service.GetDataOnly(account);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] User model)
        {
            model.UpdateTime = DateTime.Now;
            var result = await _service.Add(model);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] User model)
        {
            var result = await _service.Update(model);
            return Ok(result);
        }

        [HttpGet("GetAuthorizeByUser")]
        public async Task<IActionResult> GetAuthorizeByUser(string account)
        {
            var result = await _service.GetAllRoleByAccount(account);
            return Ok(result);
        }

        [HttpPut("UpdateAuthorizeByUser")]
        public async Task<IActionResult> UpdateAuthorizeByUser([FromBody] List_RoleUserParam list_RoleUserParam)
        {
            var result = await _service.UpdateAuthorization(list_RoleUserParam);
            return Ok(result);
        }
    }
}