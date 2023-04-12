using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.report;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.report
{
    public interface I_SortSumReport_Services
    {
        Task<PaginationUtility<SortSumReportDTO>> GetData(PaginationParam pagination, SortSumReportParam param, bool isPaging = true);
        Task<List<BrandDTO>> GetBrand();
        Task<byte[]> ExportExcel(PaginationParam pagination, SortSumReportParam param);
        Task<byte[]> ExportDetailExcel(SortSumDetailReportParam param);
    }
}