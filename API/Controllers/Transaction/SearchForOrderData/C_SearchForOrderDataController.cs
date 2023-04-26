
using API._Services.Interfaces.Transaction.SearchForOderData;
using API.DTOs.Transaction.SearchForOrderData;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Transaction.SearchForOrderData
{
    public class C_SearchForOrderDataController : APIController
    {
        private readonly ISearchForOrderDataService _service;

        public C_SearchForOrderDataController(ISearchForOrderDataService service)
        {
            _service = service;
        }
        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] SearchForOrderDataParam param)
        {
            return Ok(await _service.GetDataPagination(pagination, param));
        }
        [HttpGet("GetListPackage")]
        public async Task<IActionResult> GetListPackage() => Ok(await _service.GetListPackage());

        [HttpPost("OrderPrint")]
        public async Task<IActionResult> OrderPrint(OrderDataPrint dataPrint)
        {
            dataPrint.UserName = userName;
            return Ok(await _service.OrderPrint(dataPrint));
        }
        [HttpGet("GetListBrandname")]
        public async Task<IActionResult> GetListBrandname() => Ok(await _service.GetListBrandname());
    }
}