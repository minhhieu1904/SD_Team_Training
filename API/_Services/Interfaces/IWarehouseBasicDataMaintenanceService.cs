using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SDCores;
namespace API._Services.Interfaces
{
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IWarehouseBasicDataMaintenanceService
    {
        Task<PaginationUtility<MS_Warehouse>> GetAll(PaginationParam pagination, WarehouseBasicDataMaintenanceParam param);
        Task<OperationResult> Create(MS_Warehouse model);
        Task<OperationResult> Update(MS_Warehouse model);
        Task<PaginationUtility<MS_Warehouse>> Search(PaginationParam param, string text);
    }
}