using API._Services.Interfaces;
using API.Dtos;
using API.Dtos.Maintain.AuthorizationSetting;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Maintain
{
    public class S_AuthorizationSettingController : APIController
    {
        private readonly IAuthorizationSettingService _authorization;

        public S_AuthorizationSettingController(IAuthorizationSettingService authorization)
        {
            _authorization = authorization;
        }

        [HttpGet("getDataUsers")]
        public async Task<IActionResult> GetDataUser([FromQuery] PaginationParam pagination)
        {
            var data = await _authorization.GetDataUsers(pagination);
            return Ok(data);
        }

        [HttpGet("getRoleUsers")]
        public async Task<IActionResult> GetRoleUsers(string account)
        {
            var data = await _authorization.GetRoleUser(account);
            return Ok(data);
        }

        [HttpPost("updateAuthorUser")]
        public async Task<IActionResult> UpdateAuthorUser(List<RolesUserStatus> authors)
        {
            var data = await _authorization.UpdateAuthorUser(authors, userName);
            return Ok(data);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            userDto.update_by = userName;
            userDto.update_time = DateTime.Now;
            return Ok(await _authorization.Add(userDto));
        }

        [HttpPut("EditUser")]
        public async Task<IActionResult> Edit([FromBody] UserDto userDto)
        {
            userDto.update_by = userName;
            userDto.update_time = DateTime.Now;
            return Ok(await _authorization.Edit(userDto));
        }
    }
}