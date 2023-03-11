
using API._Services.Interfaces;
using API.Data;
using API.DTOs;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API.Controllers
{
    public class C_ShiftDataMaintenance : APIController
    {
        private readonly IShiftDataMaintenanceService _ShiftData;

        public C_ShiftDataMaintenance(IShiftDataMaintenanceService shiftData)
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