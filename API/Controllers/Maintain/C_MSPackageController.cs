using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SDCores;

namespace API.Controllers
{
    public class C_MSPackageController : APIController
    {
        private readonly IMSPackageServices _services;
        public C_MSPackageController(IMSPackageServices services)
        {
            _services = services;
        }

        [HttpGet("GetDataPaing")]
        public async Task<IActionResult> GetData([FromQuery] PaginationParam param, string packageNo, decimal packageQty)
        {
            var result = await _services.GetDataPaing(param, packageNo, packageQty);
            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddNew([FromBody] MS_Package model)
        {
            var result = await _services.Add(model);
            return Ok(result);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> UpdateItem([FromBody] MS_Package model)
        {
            var result = await _services.Update(model);
            return Ok(result);
        }

        [HttpGet("GetItem")]
        public async Task<IActionResult> GetItem(string manuf, string packageNo)
        {
            var result = await _services.GetItem(manuf, packageNo);
            return Ok(result);
        }
    }
}