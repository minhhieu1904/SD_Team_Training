using API.DTOs.Maintain;
using SDCores;

namespace API._Services.Interfaces.Maintain
{
    public interface I_1_2_WarehouseBasicDataMaintenance
    {
        public Task<PaginationUtility<WarehouseDto>> GetDataPagination(PaginationParam pagination, WarehouseParam param);
        public Task<OperationResult> AddNew(WarehouseDto data);
        public Task<OperationResult> Edit(WarehouseDto data);
    }
}