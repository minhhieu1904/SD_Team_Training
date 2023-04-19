
using API.DTOs.Transaction.SearchForOrderData;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Transaction.SearchForOderData
{
    public interface ISearchForOrderDataService
    {
        Task<List<KeyValuePair<decimal, decimal>>> GetListPackage();
        Task<List<KeyValuePair<string, string>>> GetListBrandname();
        Task<PaginationUtility<SearchForOrderDataViewModel>> GetDataPagination(PaginationParam pagination, SearchForOrderDataParam param, bool isPaging = true);
        Task<OperationResult> OrderPrint(OrderDataPrint dataPrint);
    }
}