using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.MS_QR_Order;
using API.Helper.Params;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface ISearchForOrderDataService
    {
        Task<List<KeyValuePair<decimal, decimal>>> GetListPackage();
        Task<List<KeyValuePair<string, string>>> GetListBrandname();
        Task<PaginationUtility<SearchForOrderDataViewModel>> GetDataPagination(PaginationParam pagination, SearchForOrderDataParam param, bool isPaging = true);
        Task<OperationResult> OrderPrint(OrderDataPrint dataPrint);

    }
}