using API._Services.Interfaces.Transaction;
using API.Dtos.Transaction.SearchForOrderData;
using API.DTOs.Transaction;
using API.Helpers.Params.Transaction;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_SearchForOrderDataController : APIController
    {
        private readonly I_SearchForOrderDataServices _service;
        public C_SearchForOrderDataController(I_SearchForOrderDataServices service)
        {
            _service = service;
        }


        [HttpGet("GetDataPaing")]
        public async Task<IActionResult> GetDataPaing([FromQuery] PaginationParam pagination, [FromQuery] SearchForOrderDataParam param)
        {
            var result = await _service.GetDataPaing(pagination, param);
            return Ok(result);
        }

        [HttpPost("Print")]
        public async Task<IActionResult> Print(OrderDataPrint dataPrint)
        {
            dataPrint.UserName = userName;
            var result = await _service.OrderPrint(dataPrint);
            return Ok(result);
        }

        [HttpGet("GetListPackage")]
        public async Task<IActionResult> GetListPackage()
        {
            var result = await _service.GetListPackage();
            return Ok(result);
        }








    }
}