
using API._Services.Interfaces.Transaction.PackingScan;
using API.DTOs.Transaction.PackingScan;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Transaction.PackingScan
{

    public class C_PackingScanController : APIController
    {
        private readonly IPackingScanService _service;

        public C_PackingScanController(IPackingScanService service)
        {
            _service = service;
        }
        [HttpGet("getList")]
        public async Task<IActionResult> GetList([FromQuery]PaginationParam pagination,[FromQuery] string TransactionNo)
        {
            return Ok(await _service.GetList(pagination, TransactionNo));
        }
        [HttpGet("checkScanItem")]
        public async Task<IActionResult> CheckScanItem(string scanText)
        {
            return Ok(await _service.CheckScanItem( scanText));
        }
        [HttpPost("savePackingScanList")]
        public async Task<IActionResult> SavePackingScanList(PackingScanDTOParam data)
        {
            return Ok(await _service.SavePackingScanList( data,userName));
        }
        [HttpGet("GetDataExport")]
        public async Task<IActionResult> GetDataExport(string TransactionNo)
        {
            return Ok(await _service.GetDataExport(TransactionNo));
        }

        [HttpGet("getListShift")]
        public async Task<IActionResult> getListShift() => Ok(await _service.getListShift());
    }
}