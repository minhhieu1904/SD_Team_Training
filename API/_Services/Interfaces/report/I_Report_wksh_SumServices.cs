using API.DTOs;
using API.DTOs.Report;
using API.Models;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_Report_wksh_SumServices
    {
        Task<List<Report_Wksh_SumResult>> GetData(ReportWkshSum model);
        Task<PaginationUtility<Report_Wksh_SumResult>> GetDataPaging(PaginationParam param, ReportWkshSum model);
        Task<List<GetBrand>> GetBrands();
        Task<OperationResult> ExportExcel(ReportWkshSum param, string userName);
    }
}