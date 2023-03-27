using API.Models;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface IMSPackageServices
    {
        Task<PaginationUtility<MS_Package>> GetDataPaing(PaginationParam param, string packageNo, decimal packageQty);
        Task<OperationResult> Add(MS_Package model);
        Task<OperationResult> Update(MS_Package model);
        Task<MS_Package> GetItem(string manuf, string packageNo);
    }
}