using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.Maintain
{
    public class C_shiftController : APIController
    {
         private readonly IShiftDataMaintenanceService _ShiftData;

        public C_shiftController(IShiftDataMaintenanceService shiftData)
        {
            _ShiftData = shiftData;
        }

        [HttpGet("getdata")]
        public async Task<ActionResult> GetMs_Shift([FromQuery]PaginationParam pagination, string shift, string shiftName)
        {
            var data = await _ShiftData.LoadData(pagination, shift, shiftName);
            return Ok(data);                
        }


        [HttpPost("add")]
         public async Task<ActionResult> Add([FromBody] MS_Shift shift )
        {
            // Manuf mặc định tự truyền N
            shift.Manuf = "N";
            var data = await _ShiftData.Add(shift);
            return Ok(data);  
        }


        [HttpPut("update")]
         public async Task<ActionResult> Update([FromBody] MS_Shift shift )
        {       
            var data = await _ShiftData.Update(shift);
            return Ok(data);  
        }
     
    }
}