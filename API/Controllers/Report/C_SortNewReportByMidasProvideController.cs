using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Report
{
    public class C_SortNewReportByMidasProvideController : APIController
    {
        private readonly I_SortNewReportByMidasProvideService _service;
        public C_SortNewReportByMidasProvideController(I_SortNewReportByMidasProvideService service)
        {
            _service = service;
        }

        [HttpGet("ExportExcel")]
        public async Task<IActionResult> ExportExcel(string sortDate)
        {
            var res = await _service.ExportExcel(sortDate, userName);
            return Ok(res);
        }

    }
}