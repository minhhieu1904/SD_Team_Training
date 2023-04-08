
using API.DTOs.Report;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface IReport_wksh_SumService
    {
        Task<PaginationUtility<Report_wksh_SumParam>> GetData(PaginationParam pagination ,Report_wksh_SumResult_Param param, bool isPaging = true);
        Task<List<getBrand>> GetBrand();
    }
}