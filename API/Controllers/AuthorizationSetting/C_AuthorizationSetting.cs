using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.AnthorizationSetting
{
    public class C_AuthorizationSetting : APIController
    {
        private readonly I_AuthorizationSetting _services;
        public C_AuthorizationSetting(I_AuthorizationSetting services)
        {
            _services = services;
        }
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,string account, string name)
        {
            var result = await _services.GetData(pagination,account,name);
            return Ok(result);
        }
        

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]Users model)
        {
            model.update_time = DateTime.Now;
            var result = await _services.Addnew(model);
            return Ok(result);
        }

        [HttpGet("getDataOnly")]
        public async Task<IActionResult> GetDataOnly(string account)
        {
            var result = await _services.GetDataOnly(account);
            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody]Users model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }
        [HttpGet("GetAuthorizeByUser")]
        public async Task<IActionResult> GetAuthorizeByUser(string account)
        {
            var result = await _services.GetAllRoleByAccount(account);
            return Ok(result);
        }

        [HttpPut("UpdateAuthorizeByUser")]
        public async Task<IActionResult> UpdateAuthorizeByUser([FromBody] UserRoleDTO authors)
        {
            var result = await _services.UpdateAuthorization(authors);
            return Ok(result);
        }
    }
}