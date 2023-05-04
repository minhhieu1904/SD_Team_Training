using API._Services.Interfaces.Transaction;
using API.DTOs.Trainsaction;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Transaction
{
    public class C_SearchForOrderDataController : APIController
    {
        private readonly I_SearchForOrderDataServices _service;

        public C_SearchForOrderDataController(I_SearchForOrderDataServices service)
        {
            _service = service;
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam pagination, [FromQuery] SearchForOrderDataParam param)
        {
            var result = await _service.GetDataPagination(pagination, param);
            return Ok(result);
        }

        [HttpPost("Print")]
        public async Task<IActionResult> Print(OrderDataPrint dataPrint)
        {
            dataPrint.UserName = "admin";
            return Ok(await _service.Print(dataPrint));
        }
    }
}