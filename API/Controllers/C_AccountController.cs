using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Maintain
{
    
    public class C_AccountController : APIController
    {
        
         private readonly I_UserRoleServices _userRoledata;

        public C_AccountController(I_UserRoleServices UserRoledata)
        {
            _userRoledata = UserRoledata;
        }
        [HttpGet("Search")]
        public async Task<ActionResult> GetMs_UserRole([FromQuery] PaginationParam pagination, string account, string name)
        {
            var data = await _userRoledata.Search(pagination, account, name);
            return Ok(data);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserDTO user)
        {
            user.update_by = "admin";
            user.update_time = DateTime.Now;
            var data = await _userRoledata.Create(user);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<ActionResult> Update([FromBody] UserDTO user)
        {
            user.update_time = DateTime.Now;
            user.update_by = "admin";
            var data = await _userRoledata.Update(user);
            return Ok(data);
        }

        [HttpGet("GetAuthorizeByUser")]
        public async Task<IActionResult> GetAuthorizeByUser(string account)
        {
            var result = await _userRoledata.GetAllRoleByAccount(account);
            return Ok(result);
        }

        [HttpPut("UpdateAuthorizeByUser")]
        public async Task<IActionResult> UpdateAuthorizeByUser([FromBody] UserRoleDTO authors)
        {
            var result = await _userRoledata.UpdateAuthorization(authors);
            return Ok(result);
        }
    }
}