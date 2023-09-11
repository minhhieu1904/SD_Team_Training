
using API._Services.Interfaces;
using API.DTOs.ShiftDataMaintain;
using API.Helper.Params.ShiftDataMaintain;
using Microsoft.AspNetCore.Mvc;
using SDCores;
namespace API.Controllers
{
    public class ShiftDataMaintainController : APIController
    {
        private readonly IShiftDataMaintainService _service;
        public ShiftDataMaintainController(IShiftDataMaintainService service)
        {
            _service = service;
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, [FromQuery] ShiftDataMaintainParam param)
        {
            var result = await _service.GetDataPagination(pagination, param);
            return Ok(result);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] MS_ShiftDTO dto)
        {
            var result = await _service.Create(dto);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] MS_ShiftDTO dto)
        {
            var result = await _service.Update(dto);
            return Ok(result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] MS_ShiftDTO dto)
        {
            var result = await _service.Delete(dto);
            return Ok(result);
        }

        [HttpGet("DownloadExcel")]
        public async Task<IActionResult> Download([FromQuery] ShiftDataMaintainParam param)
        {
            // param.Factory = _configuration.GetSection("Appsettings:Factory_Code").Value;
            var result = await _service.DownloadFileExcel(param);
            return await Task.FromResult(File(result, "application/xlsx", $"Excel_{DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss")}.xlsx"));
        }

        [HttpPost("UploadExcel")]
        public async Task<IActionResult> UploadData([FromForm] ShiftDataMaintainUploadParam param)
        {
            // param.Factory = _configuration.GetSection("Appsettings:Factory_Code").Value;
            return Ok(await _service.UploadFileExcel(param, userName));
        }

    }
}