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
    public interface I_WkshSumReport_Services
    {
        Task<PaginationUtility<Report_wksh_SumResult>> GetData(PaginationParam pagination, WkshSumReport param, bool isPaging = true);
        Task<List<BrandDTO>> GetBrand();
        Task<byte[]> ExportExcel(PaginationParam pagination, WkshSumReport param);
    }
}