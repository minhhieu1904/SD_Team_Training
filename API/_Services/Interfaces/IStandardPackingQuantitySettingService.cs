using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SDCores;
namespace API._Services.Interfaces
{
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IStandardPackingQuantitySettingService
    {
        Task<PaginationUtility<MS_Package>> GetAll(PaginationParam pagination, StandardPackingQuantitySettingParam param);
        Task<PaginationUtility<MS_Package>> Search(PaginationParam pagination, string text);
        Task<OperationResult> Create(MS_Package model);
        Task<OperationResult> Update(MS_Package model);
    }
}