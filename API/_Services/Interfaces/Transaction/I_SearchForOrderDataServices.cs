
using API.DTOs;
using API.DTOs.Trainsaction;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Transaction
{
    public interface I_SearchForOrderDataServices
    {
        Task<PaginationUtility<SearchForOrderDataDTO>> GetDataPagination(PaginationParam pagination, SearchForOrderDataParam param);
        Task<OperationResult> Print(OrderDataPrint dataPrint);
    }
}