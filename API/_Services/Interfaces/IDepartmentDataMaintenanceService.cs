using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SDCores;
namespace API._Services.Interfaces
{
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IDepartmentDataMaintenanceService
    {
        Task<PaginationUtility<MS_Department>> GetAll(PaginationParam pagination, DepartmentDataMaintenanceParam param);
        Task<OperationResult> Create (MS_Department model);
        Task<OperationResult> Update (MS_Department model);
        Task<PaginationUtility<MS_Department>> Search (PaginationParam pagination, string text);
    }
}