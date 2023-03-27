
using API.Dtos.Transaction.PackingScan;
using API.DTOs.Transaction;
using SDCores;


namespace API._Services.Interfaces.Transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_PackingScanServices
    {
        Task<SDCores.OperationResult> SavePackingScanList(PackingScanDto data, string username);
        Task<SDCores.OperationResult> CheckScanItem(string scanText);
        Task<SDCores.PaginationUtility<PackingScanViewDto>> GetDataPage(SDCores.PaginationParam param, string transactionNo);
        Task<List<PackingScanExportDto>> GetDataExport(string transactionNo);
    }
}