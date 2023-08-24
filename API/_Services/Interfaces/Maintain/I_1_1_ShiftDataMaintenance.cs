using API.DTOs.Maintain;
using SDCores;

namespace API._Services.Interfaces.Maintain
{
    public interface I_1_1_ShiftDataMaintenance
    {
        public Task<PaginationUtility<ShiftDataMaintenanceDto>> GetDataPagination(PaginationParam pagination, ShiftDataMaintenanceParam param);
        public Task<OperationResult> AddNew(ShiftDataMaintenanceDto data);
        public Task<OperationResult> Edit(ShiftDataMaintenanceDto data);
    }
}