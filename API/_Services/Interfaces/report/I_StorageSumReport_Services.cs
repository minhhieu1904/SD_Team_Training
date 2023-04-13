using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.DTOs.report;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.report
{
    public interface I_StorageSumReport_Services
    {
        Task<PaginationUtility<StorageSumReportDTO>> GetData(PaginationParam pagination, StorageSumReportParam param,bool isPaging = true);
        Task<byte[]> ExportExcel(PaginationParam pagination, StorageSumReportParam param);
        Task<byte[]> ExportDetailExcel(StorageSumReportDetailParam param);
        Task<List<BrandDTO>> GetBrand();
        
    }
}