
using API.DTOs.Report;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface IReport_QRCODE_WIPservices
    {
         Task<PaginationUtility<QRCODEWIPDetailReportDTO>> GetDataPagination(PaginationParam pagination, ReportQRCODEWIPParam param);

          Task<byte[]> ExportExcel(ReportQRCODEWIPParam param);
    }
}