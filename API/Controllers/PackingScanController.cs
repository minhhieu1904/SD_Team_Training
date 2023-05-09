using System.Security.Claims;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.PackingScan;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class PackingScanController : APIController
    {
        private readonly IPackingScanService _service;
        public PackingScanController(IPackingScanService service)
        {
            _service = service;
        }

        // [HttpGet("GetData")]
        // public async Task<IActionResult> GetData([FromQuery] string TransactionNo, [FromQuery] PaginationParam panagition)
        // {
        //     var result = await _service.GetData(TransactionNo, panagition);
        //     return Ok(result);
        // }
        // [HttpGet("CheckScan")]
        // public async Task<IActionResult> CheckScan([FromQuery] string scanText)
        // {
        //     var result = await _service.CheckScan(scanText);
        //     return Ok(result);
        // }

        // [HttpPost("SaveScanList")]
        // public async Task<IActionResult> SaveScanList([FromBody] PackingScanDTO model)
        // {
        //     var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //     var result = await _service.SavePackingScan(model, userName);
        //     return Ok(result);
        // }

        // [HttpGet("GetDataExport")]
        // public async Task<IActionResult> GetDataExport([FromQuery] string TransactionNo, [FromQuery] PaginationParam panagition)
        // {
        //     var result = await _service.GetDataExport(TransactionNo, panagition);
        //     return Ok(result);
        // }
        [HttpGet("CheckScanItem")]
        public async Task<IActionResult> CheckScanItem([FromQuery] string scanText)
        {
            return Ok(await _service.CheckScanItem(scanText));
        }

        [HttpPost("SaveScanList")]
        public async Task<IActionResult> SaveScanList([FromBody] PackingScanDTO data)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await _service.SavePackingScanList(data, userName));
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList([FromQuery]string TransactionNo, [FromQuery] PaginationParam paginationParam)
        {
            return Ok(await _service.GetList(paginationParam, TransactionNo));
        }

        [HttpGet("GetDataExport")]
        public async Task<IActionResult> GetDataExport([FromQuery] string TransactionNo){
            return Ok(await _service.GetDataExport(TransactionNo));
        }
    }
}