
using API.DTOs.Report;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface IReport_Storage_Sumservice
    {
        Task<PaginationUtility<Report_Storage_SumParam>> GetData(PaginationParam pagination ,Storage_sumDTO param, bool isPaging = true);
        Task<List<getBrand>> GetBrand();
        Task<byte[]> ExportExcelDetails( StorageSumDeltailDTOParam param);
    }
}