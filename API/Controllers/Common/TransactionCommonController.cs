using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common
{
    public class TransactionCommonController : APIController
    {
        private readonly ITransactionCommonService _service;
        public TransactionCommonController(ITransactionCommonService service)
        {
            _service = service;
        }

        [HttpGet("GetListShift")]
        public async Task<IActionResult> GetListShift()
        {
            var result = await _service.GetListShift();
            return Ok(result);
        }
    }
}