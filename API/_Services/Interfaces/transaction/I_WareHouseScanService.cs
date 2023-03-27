using API.DTOs.Transaction.WarehouseScan;
using SDCores;

namespace API._Services.Interfaces.Transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_WareHouseScanService
    {
        Task<OperationResult> CheckScanCode(string target);
        Task<OperationResult> SaveQRStorage(WarehouseScanDto data);
        Task<PaginationUtility<MS_QR_StorageDto>> GetListQRStorage(PaginationParam pagination, string trNo);
        Task<List<WarehouseScanExportDto>> GetQRStorageExport(string trNo);
    }
}