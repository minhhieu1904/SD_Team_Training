using System.Security.Claims;
using API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.PackingScan;

namespace API.Controllers
{
    public class PackingScanController : APIController
    {
        private readonly IPackingScanService _service;
        public PackingScanController(IPackingScanService service)
        {
            _service = service;
        }

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
        public async Task<IActionResult> GetList([FromQuery]string TransactionNo, [FromQuery] SDCores.PaginationParam paginationParam)
        {
            return Ok(await _service.GetList(paginationParam, TransactionNo));
        }

        [HttpGet("GetDataExport")]
        public async Task<IActionResult> GetDataExport([FromQuery] string TransactionNo){
            return Ok(await _service.GetDataExport(TransactionNo));
        }
    }
}