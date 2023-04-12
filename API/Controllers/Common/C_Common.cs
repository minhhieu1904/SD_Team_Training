using API._Services.Interfaces.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Common
{
    public class C_Common : APIController
    {
        private readonly I_CommonService _commonService;
        public C_Common(I_CommonService commonService)
        {
            _commonService = commonService;
        }

        [HttpGet("GetListBrandName")]
        public async Task<IActionResult> GetListBrandName()
        => Ok(await _commonService.GetListBrandName());
    }
}