using API.Dtos.Report.Report_Sort_Sum;
using API.DTOs.Report;
using API.Helpers;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface I_StorageSumReport
    {
        public Task<PaginationUtility<Report_Storage_SumResult>> Search(PaginationParam pagination, Report_Storage_Sum_Param param, bool isPaging = true);
        public Task<List<Export_Detail_Excel>> ExportDetailExcel(ExportDetailExcelParams param);
    }
}