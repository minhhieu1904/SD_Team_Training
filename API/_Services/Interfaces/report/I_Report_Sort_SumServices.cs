using API.DTOs.Report;
using API.Models;
using SDCores;

namespace API._Services.Interfaces.report
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_Report_Sort_SumServices
    {
        Task<PaginationUtility<Report_Sort_SumResult>> GetDataPaging(PaginationParam param, ReportSortSum model);
        Task<List<Report_Sort_SumResult>> GetData(ReportSortSum model);
        Task<List<GetBrand>> GetBrands();
        Task<List<ReportSortSumExcelDetail>> GetDataDetail(string manno, string purno, string size);
        Task<OperationResult> ExcelExport(ReportSortSum model, string userName);
        Task<OperationResult> ExcelExportDetail(string manno, string purno, string size, string userName);
    }
}