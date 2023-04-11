using API.DTOs.WkshSumReportDto;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface I_WksSuReportService
    {
        Task<PaginationUtility<Report_wksh_SumResult>> SearchWithPagination(PaginationParam pagination, WkshSumReportParam param, bool isPaging = true);
        // Task<byte[]> DownloadExcel(PaginationParam pagination, WkshSumReportParam param);
    }
}