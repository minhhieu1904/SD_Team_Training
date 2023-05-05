using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.report.QRCodeWIPReport;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.report
{
    public interface I_QRCodeWIPReportServices
    {
        Task<byte[]> ExportExcel(QRCodeParam param);
        Task<PaginationUtility<CheckSumMissDetailDTO>> GetDataPagination(PaginationParam pagination, QRCodeParam param, bool isPaing = true);
    }
}