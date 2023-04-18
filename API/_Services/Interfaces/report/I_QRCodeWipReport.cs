using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Report;
using Microsoft.AspNetCore.Mvc;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.report
{
    public interface I_QRCodeWipReport
    {
        Task<PaginationUtility<CheckSumMiss_TB2>> GetDataPagination([FromQuery] PaginationParam pagination, CheckSumMissReportParam param);

        Task<byte[]> ExportExcel(CheckSumMissReportParam param);
    }
}