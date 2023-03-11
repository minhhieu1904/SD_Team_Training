

using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IWarehouseBasicdataService
    {
        
        Task<PaginationUtility<MS_Location>> LoadData(PaginationParam paginationParams, string StoreH, string LocationName);
        Task<OperationResult> Add(MS_Location location);
        Task<OperationResult> Update(MS_Location location);
    }
}