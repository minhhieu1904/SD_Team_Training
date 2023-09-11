using API.DTOs.PackingScan;
using SDCores;
namespace API._Services.Interfaces
{
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IPackingScanService
    {
        Task<OperationResult> SavePackingScanList(PackingScanDTO data, string userName);
        Task<OperationResult> CheckScanItem(string scanText);
        Task<PaginationUtility<ViewDataPackingScan>> GetList(PaginationParam paginationParam, string TransactionNo);
        Task<List<PackingScanExportDTO>> GetDataExport(string TransactionNo);
    }
}