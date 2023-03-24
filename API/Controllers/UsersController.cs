using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class UsersController : APIController
    {
        private readonly IUsersServices _services;
        public UsersController(IUsersServices services)
        {
            _services = services;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam param, string account, string name)
        {
            var result = await _services.GetDataPaing(param, account, name);
            return Ok(result);
        }

        [HttpPost("AddNew")]
        public async Task<IActionResult> AddNew([FromBody] Users model)
        {   
            model.UpdateBy = "admin";
            model.UpdateTime = DateTime.Now;
            var result = await _services.AddNew(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] Users model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }

        [HttpGet("GetItem")]
        public async Task<IActionResult> GetItem(string account, string name)
        {
            var result = await _services.GetItem(account, name);
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