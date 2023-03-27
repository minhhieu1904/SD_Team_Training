using API.Models;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface IMSLocationServices
    {
        Task<List<MS_Location>> GetAllData();
        Task<OperationResult> AddNew(MS_Location model);
        Task<OperationResult> UpdateLocation(MS_Location model);
        Task<PaginationUtility<MS_Location>> GetDataPagination(PaginationParam param, string storeH, string locationName);
        Task<MS_Location> GetItem(string manuf, string storeH);
    }
}