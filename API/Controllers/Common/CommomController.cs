using API._Services.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common
{
    public class CommomController : APIController
    {
        private readonly I_CommonService _commonService;
        public CommomController(I_CommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("GetListBrandName")]
        public async Task<IActionResult> GetListBrandName()
        => Ok(await _commonService.GetListBrandName());
    }
}