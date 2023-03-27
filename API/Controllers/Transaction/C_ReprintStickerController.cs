using API._Services.Interfaces.transaction;
using API.Dtos.Transaction.ReprintSticker;
using API.Helpers.Params.Transaction;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_ReprintStickerController : APIController
    {
        private readonly I_ReprintStickerService _service;

        public C_ReprintStickerController(I_ReprintStickerService service)
        {
            _service = service;
        }

        [HttpPost("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, ReprintStickerParam param)
        {
            var result = await _service.GetData(pagination, param);
            return Ok(result);
        }

        [HttpPost("UpdateData")]
        public async Task<IActionResult> UpdateData(List<ReprintStickerModel> data)
        {
            var result = await _service.UpdateData(data);
            return Ok(result);
        }

        [HttpPost("GetDataByScan")]
        public async Task<IActionResult> GetDataByScan(List<ReprintStickerModel> listModel)
        {
            var result = await _service.GetDataByScan(listModel);
            return Ok(result);
        }
    }
}