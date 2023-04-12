using API._Services.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common
{
    public class CommonController : APIController
    {
        private readonly I_CommonService _commonService;
        public CommonController(I_CommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("GetListBrandName")]
        public async Task<IActionResult> GetListBrandName()
        => Ok(await _commonService.GetListBrandName());
    }
}