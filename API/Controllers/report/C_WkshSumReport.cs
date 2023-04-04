using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces.report;
using API.DTOs.report;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API.Controllers.report
{
    public class C_WkshSumReport : APIController
    {
       private I_WkshSumReport_Services _services;

        public C_WkshSumReport ( I_WkshSumReport_Services services)
        {
            _services = services;
        } 
        [HttpGet("getData")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParam pagination ,[FromQuery] WkshSumReport param,bool isPaging = true)
        {
            var result = await _services.GetData(pagination,param,isPaging);
            return Ok(result);
        }
        // [HttpGet("getBrand")]
        // public async Task<IActionResult> GetBrand()
        // {
        //     var result = await _services.GetBrand();
        //     return Ok(result);
        // }
    }
}