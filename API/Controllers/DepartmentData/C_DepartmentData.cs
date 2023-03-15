
using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.DepartmentData
{
 
    public class C_DepartmentData : APIController
    {
        private readonly IDepartmentDataServices _departmentData;

        public C_DepartmentData(IDepartmentDataServices departmentData)
        {
            _departmentData = departmentData;
        }
        [HttpGet("getData")]
        public async Task<ActionResult> GetMs_Department([FromQuery]PaginationParam pagination, string parno, string parName )
        {
            var data = await _departmentData.LoadData(pagination, parno, parName);
            return Ok(data);
        }
        [HttpPost("add")]
         public async Task<ActionResult> Add([FromBody] MS_Department parno)
        {
            parno.Manuf = "N";
            var data = await _departmentData.Add(parno);
            return Ok(data);
        }
          [HttpPut("upDate")]
         public async Task<ActionResult> Update([FromBody] MS_Department parno ){
            var data = await _departmentData.Update(parno);
            return Ok(data);
        }
        
    }
}