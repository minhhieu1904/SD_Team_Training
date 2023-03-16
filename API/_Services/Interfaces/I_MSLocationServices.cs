using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IMSLocationServices
    {
        Task <PaginationUtility<MS_Location>> GetDataPaging(PaginationParam param, string storeH, string locationName);
        Task <OperationResult> AddNew(MS_Location model);
        Task <OperationResult> UpdateWarehouse(MS_Location model);
        Task<MS_Location> GetItem(string manuf, string storeH);
    }
}