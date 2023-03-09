using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class MSShiftController: APIController
    {
        private readonly IMSShiftServices _services;

        public MSShiftController(IMSShiftServices services)
        {
            _services = services;
        }

        // Hien thi data bang MS_Shift
        [HttpGet("GetData")]
        public async Task<IActionResult> GetData() { 
            var result = await _services.GetAllShift();
            return Ok(result);
        }

        //Them moi
        [HttpPost("AddNew")]
        public async Task<IActionResult> Add([FromBody]MS_Shift model) {
            var result = await _services.AddNewShift(model);
            return Ok(result);
        }

        //Sua
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody]MS_Shift model) {
            var result = await _services.UpdateShift(model);
            return Ok(result);
        }

        //Xoa
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete([FromBody]MS_Shift model) {
            var result = await _services.Delete(model);
            return Ok(result);
        }

        //Phan trang
        [HttpGet("GetDataPaging")]
        public async Task<IActionResult> GetDataPaging([FromQuery]PaginationParam param, string shift, string shiftName) {
            var result = await _services.GetDataPagination(param, shift, shiftName);
            return Ok(result);
        }
    }
}