using System.Security.Claims;
using API._Services.Interfaces.Transaction;
using API.DTOs.Transaction;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_PackingScanController : APIController
    {
        private readonly I_PackingScanServices _service;

        public C_PackingScanController(I_PackingScanServices service)
        {
            _service = service;
        }

        [HttpGet("CheckScanItem")]
        public async Task<IActionResult> CheckScanItem([FromQuery] string scanText)
        {
            return Ok(await _service.CheckScanItem(scanText));
        }

        [HttpPost("SaveScanList")]
        public async Task<IActionResult> SaveScanList([FromBody] PackingScanDto data)
        {
            var userName = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return Ok(await _service.SavePackingScanList(data, userName));
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList([FromQuery] string transactionNo, [FromQuery] PaginationParam param)
        {
            return Ok(await _service.GetDataPage(param, transactionNo));
        }

        [HttpGet("GetDataExport")]
        public async Task<IActionResult> GetDataExport([FromQuery] string transactionNo)
        {
            return Ok(await _service.GetDataExport(transactionNo));
        }


    }
}