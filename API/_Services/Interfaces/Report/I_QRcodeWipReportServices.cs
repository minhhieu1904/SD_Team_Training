using API.DTOs.Report;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;
namespace API._Services.Interfaces.Report
{
    public interface I_QRcodeWipReportServices
    {
        Task<PaginationUtility<CheckSumMissDetailReportDTO>> GetDataPagination([FromQuery] PaginationParam pagination, CheckSumMissReportParam param);

        Task<byte[]> ExportExcel(CheckSumMissReportParam param);
    }
}