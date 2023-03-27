using API.Models;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface IMSDepartmentServices
    {
        Task<PaginationUtility<MS_Department>> GetPagingData(PaginationParam param, string parNo, string parName);
        Task<OperationResult> AddNew(MS_Department model);
        Task<OperationResult> Update(MS_Department model);
        Task<MS_Department> GetItem(string manuf, string parNo);
    }
}