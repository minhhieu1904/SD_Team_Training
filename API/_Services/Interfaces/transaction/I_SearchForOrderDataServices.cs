using API.Dtos.Transaction.SearchForOrderData;
using API.Helpers.Params.Transaction;
using SDCores;

namespace API._Services.Interfaces.Transaction
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_SearchForOrderDataServices
    {
        Task<List<KeyValuePair<decimal, decimal>>> GetListPackage();
        Task<PaginationUtility<SearchForOrderDataViewModel>> GetDataPaing(PaginationParam pagination, SearchForOrderDataParam param);
        Task<OperationResult> OrderPrint(OrderDataPrint dataPrint);
    }
}