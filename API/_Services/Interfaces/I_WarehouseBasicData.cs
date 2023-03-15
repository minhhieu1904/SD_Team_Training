
using API.DTOs;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_WarehouseBasicData
    {
        Task<PaginationUtility<MS_Location>> getData(PaginationParam pagination, WarehouseBasicData param);

        Task<OperationResult> addNew(MS_Location model);
        Task<OperationResult> Update(MS_Location model);

        Task<MS_Location> getDataOnly(string manuf, string StoreH);



    }
}