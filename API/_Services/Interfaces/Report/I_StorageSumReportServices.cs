using API.DTOs;
using API.DTOs.Report;
using SD3_API.Helpers.Utilities;
namespace API._Services.Interfaces.Report
{
    public interface I_StorageSumReportServices
    {
        Task<PaginationUtility<StorageSumReportDTO>> GetDataPagination( PaginationParam pagination, StorageSumReportParam param);

        Task<List<BrandDTO>> GetBrands();

        Task<byte[]> ExportExcel(StorageSumReportParam param);

        Task<byte[]> ExportDetailExcel(StorageSumDetailReportParam param);
    }
}