
using API.DTOs.report;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.report
{
    public interface I_WkshSumReport
    {
        Task<PaginationUtility<Report_wksh_SumResult>> GetData(PaginationParam pagination ,WkshSumReport param, bool isPaging = true);
        Task<List<getBrand>> GetBrand();
        




    }
}