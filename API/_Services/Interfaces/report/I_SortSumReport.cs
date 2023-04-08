
using API.DTOs.report;
using API.DTOs.report.SortSumReport;
using API.Models;
using API.Models.report;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.report
{
    public interface I_SortSumReport
    {
        Task<PaginationUtility<Report_Sort_SumResult>> GetData(PaginationParam pagination ,SortSumReport param, bool isPaging = true);
        Task<List<SortSumReportDetail>> GetDataDetail(SortSumDetailReportParam param);
        Task<List<getBrand>> GetBrand();

    }
}