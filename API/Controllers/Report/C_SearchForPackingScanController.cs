using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Report
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_SearchForPackingScanController : ControllerBase
    {
        private readonly I_SearchForPackingScanServices _service;

        public C_SearchForPackingScanController(I_SearchForPackingScanServices service)
        {
            _service = service;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetDataPagination([FromQuery]PaginationParam  pagination,[FromQuery] SearchForPackingScanParam param){
            var result = await _service.GetDataPagination(pagination, param);
            return Ok(result);
        }

        [HttpGet("GetBrand")]
        public async Task<IActionResult> GetBrand(){
            var result = await _service.GetBrand();
            return Ok(result);
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel([FromQuery] SearchForPackingScanParam param){
            var result = await _service.ExportExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }
    }
}