using API.DTOs;
using API.DTOs.Report;
using SD3_API.Helpers.Utilities;
namespace API._Services.Interfaces.Report
{
    public interface I_SortSumReportServices
    {
        Task<PaginationUtility<SortSumReportDTO>> GetDataPagination(PaginationParam pagination, SortSumReportParam param);

        Task<byte[]> ExportExcel(SortSumReportParam param, string userName);

        Task<byte[]> ExportDetailExcel(SortSumDetailReportParam param, string userName);
    }
}