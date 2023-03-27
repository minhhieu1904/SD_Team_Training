using API.Dtos.Transaction.PickingScan;
using API.Dtos.Transaction.WarehouseOutScan;
using API.Helpers.Params.Transaction;
using API.Models;
using SDCores;

namespace API._Services.Interfaces.transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_WarehouseOutScanService
    {
        Task<PaginationUtility<MS_QR_PickingMain>> GetDataMain(PaginationParam pagination, WarehouseOutScanParam param);
        Task<PaginationUtility<PickingDetailDto>> GetPickingFromMainWarehouse(PaginationParam pagination, PickingScanItemParam param);
        Task<OperationResult> StorageOut(List<MS_QR_PickingMain> listStorageOut, string currentUser);
        Task<List<PickingDetailOutExportData>> ExportExcel(List<MS_QR_PickingMain> exportData);
    }
}