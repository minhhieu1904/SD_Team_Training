
using API._Services.Interfaces.common;
using API._Services.Services.common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class C_CommonController : APIController
    {
        private readonly I_CommonServices _services;

        public C_CommonController(I_CommonServices services)
        {
            _services = services;
        }


        [HttpGet("GetListBrandName")]
        public async Task<IActionResult> GetListBrandName()
        {
            var result = await _services.GetListBrandName();
            return Ok(result);
        }

        [HttpGet("GetListLocationName")]
        public async Task<IActionResult> GetListLocationName()
        {
            var result = await _services.GetListLocationName();
            return Ok(result);
        }

        [HttpGet("GetListDepartmentName")]
        public async Task<IActionResult> GetListDepartmentName()
        {
            var result = await _services.GetListDepartmentName();
            return Ok(result);
        }

        [HttpGet("GetListShift")]
        public async Task<IActionResult> GetListShift()
        {
            var result = await _services.GetListShift();
            return Ok(result);
        }
        [HttpGet("GetListTolcls")]
        public async Task<IActionResult> GetListTolcls()
        {
            var result = await _services.GetListTolcls();
            return Ok(result);
        }
    }
}