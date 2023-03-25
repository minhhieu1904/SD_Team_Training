using API.DTOs.WarehouseBasicData;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_WarehouseBasicDataServices
    {
        Task<PaginationUtility<MsLocation>> GetData(PaginationParam pagination, WarehouseBasicDataParam param);

        Task<MsLocation> GetDataOnly(string manuf, string StoreH);

        Task<OperationResult> Add(MsLocation model);

        Task<OperationResult> Update(MsLocation model);

        Task<OperationResult> Delete(string StoreH);
    }
}