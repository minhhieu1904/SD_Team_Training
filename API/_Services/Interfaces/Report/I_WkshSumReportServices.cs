using API.DTOs;
using SD3_API.Helpers.Utilities;
namespace API._Services.Interfaces
{
    public interface I_WkshSumReportServices
    {
        Task<PaginationUtility< WkshSumReportDTO>> GetDataPagination(PaginationParam pagination, WkshSumReportParam param);

        Task<byte[]> ExportExcel(WkshSumReportParam param, string userName);
    }
}