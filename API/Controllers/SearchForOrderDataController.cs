using API._Services.Interfaces;
using API.DTOs.MS_QR_Order;
using API.Helper.Params;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    public class SearchForOrderDataController : APIController
    {
        private readonly ISearchForOrderDataService _service;

        public SearchForOrderDataController(ISearchForOrderDataService service)
        {
            _service = service;
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] SDCores.PaginationParam pagination, [FromQuery] SearchForOrderDataParam param)
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