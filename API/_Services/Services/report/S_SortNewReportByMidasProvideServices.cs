using API._Services.Interfaces;
using API.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.report
{
    public class S_SortNewReportByMidasProvideServices : I_SortNewReportByMidasProvideService
    {
        private readonly DBContext _dBContext;
        public S_SortNewReportByMidasProvideServices(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public async Task<OperationResult> ExportExcel(string sortDate, string name)
        {
            var data = await (
                    _dBContext.Report_New4_5_ByMidasResult.FromSqlRaw("EXEC [dbo].[Report_New4_5_ByMidas] @sdate",
                    new SqlParameter("@sdate", !string.IsNullOrEmpty(sortDate) ? Convert.ToDateTime(sortDate) :
                    (object)DBNull.Value)).ToListAsync());
            if (!data.Any())
                return new OperationResult(false, "No data");

            List<Table> listData = new()
            {
                new Table ("result", data)
            };

            ExcelResult result = ExcelUtility.DownloadExcel(listData, "Resources/Template/Report/SortNewReportByMidasProvide/4.5SortNewReportByMidasProvide.xls");
            return new OperationResult(result.IsSuccess, result.Error, result.Result);
        }

    }
}