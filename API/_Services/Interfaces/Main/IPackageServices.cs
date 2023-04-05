
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IPackageServices
    {
         Task<PaginationUtility<MS_Package>> LoadData(PaginationParam paginationParams, string packageNo, decimal packageQty);
        Task<OperationResult> Add(MS_Package packageNo);
        Task<OperationResult> Update(MS_Package packageNo);
    }
}