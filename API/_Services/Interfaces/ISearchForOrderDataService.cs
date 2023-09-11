using API.DTOs.MS_QR_Order;
using API.Helper.Params;
using SDCores;
namespace API._Services.Interfaces
{
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface ISearchForOrderDataService
    {
        Task<List<KeyValuePair<decimal, decimal>>> GetListPackage();
        Task<List<KeyValuePair<string, string>>> GetListBrandname();
        Task<PaginationUtility<SearchForOrderDataViewModel>> GetDataPagination(PaginationParam pagination, SearchForOrderDataParam param, bool isPaging = true);
        Task<OperationResult> OrderPrint(OrderDataPrint dataPrint);

    }
}