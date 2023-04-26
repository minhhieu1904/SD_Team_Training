
using API.DTOs.Transaction.SearchForCyclePackingData;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Transaction.SearchForCyclePackingData
{
    public interface ISearchForCyclePackingDataService
    {
        Task<List<KeyValuePair<decimal, decimal>>> GetListPackage();
        Task<PaginationUtility<SearchForCyclePackingDataViewModel>> GetDataPagination(PaginationParam pagination, CyclePackingPrintParam param, bool isPaging = true);
        Task<OperationResult> CyclePackingPrint(CyclePackingPrint dataPrint);
    }
}