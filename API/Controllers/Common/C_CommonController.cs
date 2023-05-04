using API._Services.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common
{
    public class C_CommonController : APIController
    {
        private readonly I_CommonServices _service;

        public C_CommonController(I_CommonServices service)
        {
            _service = service;
        }

        [HttpGet("GetListBrandName")]
        public async Task<IActionResult> GetListBrandName()
        {
            var result = await _service.GetListBrandName();
            return Ok(result);
        }

        [HttpGet("GetListPackage")]
        public async Task<IActionResult> GetListPackage()
        {
            var result = await _service.GetListPackage();
            return Ok(result);
        }

        [HttpGet("GetListStatus")]
        public async Task<IActionResult> GetListStatus()
        {
            var result = await _service.GetListStatus();
            return Ok(result);
        }
    }
}