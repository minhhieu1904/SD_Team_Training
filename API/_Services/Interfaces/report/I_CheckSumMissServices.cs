using API.Dtos.Report;
using API.DTOs.Report;
using API.Helpers.Params.Report;
using SDCores;

namespace API._Services.Interfaces.report
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_CheckSumMissServices
    {
        Task<Report_Check_Sum_Miss> GetCheckSumMissData(PaginationParam pagination, QRcodeWIPReportParam param, bool is_Paging = true);
        Task<OperationResult> ExportExcel(PaginationParam pagination, QRcodeWIPReportParam param);
    }
}