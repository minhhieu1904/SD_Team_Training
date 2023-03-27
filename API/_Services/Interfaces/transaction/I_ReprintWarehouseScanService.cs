
using API.DTOs.Transaction.WarehouseScan;
using API.DTOs.Transaction.ReprintWarehouseScan;
using SDCores;

namespace API._Services.Interfaces.transaction
{
    public interface I_ReprintWarehouseScanService
    {
        [DependencyInjection(ServiceLifetime.Scoped)]
        Task<PaginationUtility<ReprintWarehouseScanDto>> GetDataPagination(ReprintWarehouseScanParam param, PaginationParam pagination);
        Task<List<WarehouseScanExportDto>> GetDataExport(List<ReprintWarehouseScanDto> dataReprints);
    }

}