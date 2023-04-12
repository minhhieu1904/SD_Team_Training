
using API.DTOs.report.SortSumReport;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.report
{
    public interface I_StorageSumReport
    {
         Task<PaginationUtility<Report_Storage_SumResult>> GetData(PaginationParam pagination ,StorageSumReport param, bool isPaging = true);
        Task<List<StorageSumReportDetai>> GetDataDetail(StorageSumReportDetailParam param);
        Task<List<getBrand>> GetBrand();
    }
}