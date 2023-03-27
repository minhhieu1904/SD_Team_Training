using API.DTOs.Report;
using API.Models;
using SDCores;

namespace API._Services.Interfaces.report
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_Report_StorageSumService
    {
        Task<PaginationUtility<Report_Storage_SumResult>> GetDataPaing(PaginationParam param, ReportStorageSum model);
        Task<List<Report_Storage_SumResult>> GetData(ReportStorageSum model);
        Task<List<GetBrand>> GetBrand();
        Task<List<ReportStorageSumDetail>> GetDataDetail(string manno, string purno, string size);
        Task<OperationResult> ExportExcel(ReportStorageSum model, string userName);
        Task<OperationResult> ExportExcelDetail(string userName, string manno, string purno, string size);
    }
}