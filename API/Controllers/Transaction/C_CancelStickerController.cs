using API._Services.Interfaces.Transaction;
using API.Helpers.Params.Transaction;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_CancelStickerController : APIController
    {
        private readonly I_CancelStickerService _service;

        public C_CancelStickerController(I_CancelStickerService service)
        {
            _service = service;
        }

        [HttpGet("CheckRecordScan")]
        public async Task<IActionResult> CheckRecordScan(string qrCodeValue)
        {
            return Ok(await _service.CheckRecordScan(qrCodeValue));
        }

        [HttpPut("CancelStickerAction")]
        public async Task<IActionResult> CancelStickerAction(List<string> datas)
        {
            return Ok(await _service.CancelStickerAction(datas, userName));
        }

        [HttpPut("CancelStickerScan")]
        public async Task<IActionResult> CancelStickerScan(List<string> datas)
        {
            return Ok(await _service.CancelStickerScan(datas, userName));
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] CancelStickerParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }
    }
}