using API.DTOs;
using SD3_API.Helpers.Utilities;
namespace API._Services.Interfaces
{
    public interface I_SearchForPackingScanServices
    {
        Task<PaginationUtility<SearchForPackingScanDTO>> GetDataPagination(PaginationParam pagination, SearchForPackingScanParam param);

        Task<byte[]> ExportExcel(SearchForPackingScanParam param);

        Task<List<BrandDTO>> GetBrand();
    }
}