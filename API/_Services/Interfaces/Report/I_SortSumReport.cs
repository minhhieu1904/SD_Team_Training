using API.Dtos.Report.Report_Sort_Sum;
using API.DTOs.Report;
using API.Helpers.Params.SortSumReport;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface I_SortSumReport
    {
        public Task<PaginationUtility<Report_Sort_SumResult>> Search(PaginationParam pagination, SearchSortSumReportParams param, bool is_Paging = true);
        public Task<List<Export_Detail_Excel>> ExportDetailExcel(ExportDetailExcelParams param);
    }
}