using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.transaction;
using API.Dtos.Transaction.OrderDataStatusAdjust;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers.Transaction
{
    public class C_OrderDataStatusAdjustController : APIController
    {
        private readonly I_OrderDataStatusAdjustService _service;

        public C_OrderDataStatusAdjustController(I_OrderDataStatusAdjustService service)
        {
            _service = service;
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam param, [FromQuery] OrderDataStatusAdjustParam filter)
        {
            var data = await _service.GetDataPagination(param, filter);
            return Ok(data);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] OrderDataStatusAdjustUpdate model)
        {
            var result = await _service.Update(model);
            return Ok(result);
        }
    }
}