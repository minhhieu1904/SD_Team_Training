
using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;
namespace API.Controllers.Packagedata
{
    public class C_PackageData : APIController
    {
         private readonly IPackageServices _packageData;
     

        public C_PackageData(IPackageServices packageData)
        {
            _packageData = packageData;
        }
        [HttpGet("getData")]
        public async Task<ActionResult> GetMs_Package([FromQuery]PaginationParam pagination, string packageNo, decimal packageQty )
        {
            var data = await _packageData.LoadData(pagination, packageNo, packageQty);
            return Ok(data);
        }
        [HttpPost("add")]
         public async Task<ActionResult> Add([FromBody] MS_Package packageNo)
        {
            packageNo.Manuf = "N";
            var data = await _packageData.Add(packageNo);
            return Ok(data);
        }
          [HttpPut("upDate")]
         public async Task<ActionResult> Update([FromBody] MS_Package packageNo ){
            var data = await _packageData.Update(packageNo);
            return Ok(data);
        }
    }
}