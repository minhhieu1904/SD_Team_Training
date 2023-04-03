
using API.DTOs.Report;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface IPackingScanServices 
    {
        Task<PaginationUtility<Report_wksh_SumReult>> LoadData(PaginationParam paginationParams, string ParNo, string ParName);
        Task<byte[]> ExportExcel( OderParam param);

    }
}