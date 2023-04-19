using API.Dtos.Report;
using API.Helpers.Params.Report;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface I_QRCodeWIPReport
    {
        Task<Report_Check_Sum_Miss> Search(PaginationParam pagination, QRcodeWIPReportParam param, bool is_Paging = true);
    }
}