using System.Data;
using System.Globalization;
using API._Repositories;
using API._Services.Interfaces.report;
using API.Dtos.Report;
using API.Helpers.Params.Report;
using Microsoft.Data.SqlClient;
using SDCores;

namespace API._Services.Services.report
{
    public class S_CheckSumMissServices : I_CheckSumMissServices
    {
        private readonly IRepositoryAccessor _repoAccessor;
        private readonly IConfiguration _configuration;
        private string area;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public S_CheckSumMissServices(IRepositoryAccessor repoAccessor, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _repoAccessor = repoAccessor;
            _configuration = configuration;
            area = _configuration.GetSection("Appsettings:Area").Value;
        }

        public async Task<OperationResult> ExportExcel(PaginationParam pagination, QRcodeWIPReportParam param)
        {
            var data = await GetCheckSumMissData(pagination, param);
            if (!data.Headers.Any() && !data.Details.Result.Any())
                return new OperationResult(false, "No data");
            List<Table> listTable = new List<Table>()
            {
                new Table ("headers", data.Headers),
                new Table ("details", data.Details.Result)
            };
            List<Cell> listCell = new List<Cell>()
            {
                new Cell ("I2", DateTime.Now.ToString("yyyy/mm/dd")),
            };
            ExcelResult result = ExcelUtility.DownloadExcel(listTable, listCell, "Resources/Template/Report/CheckSumMissReport.xlsx");
            return new OperationResult(result.IsSuccess, result.Error, result.Result);
        }

        public async Task<Report_Check_Sum_Miss> GetCheckSumMissData(PaginationParam pagination, QRcodeWIPReportParam param, bool is_Paging = true)
        {
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString($"DefaultConnection_{area}")))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("CheckSumMiss", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("@moldno", SqlDbType.NVarChar).Value = param.moldno ?? (object)DBNull.Value;
                cmd.Parameters.Add("@toolno", SqlDbType.NVarChar).Value = param.toolno ?? (object)DBNull.Value;
                cmd.Parameters.Add("@mdat_start", SqlDbType.Date).Value = param.mdat_start ?? (object)DBNull.Value;
                cmd.Parameters.Add("@mdat_end", SqlDbType.Date).Value = param.mdat_end ?? (object)DBNull.Value;
                await cmd.ExecuteNonQueryAsync();

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable tableHeader = ds.Tables[0];
                DataTable tableDetail = ds.Tables[1];

                List<ReportCheckSumMissHeader> headers = new();
                if (!is_Paging)
                {
                    foreach (DataRow dr in tableHeader.Rows)
                    {
                        ReportCheckSumMissHeader header = new()
                        {
                            yymmdd = dr.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[0]) : null,
                            Sumpackage = dr.ItemArray[1] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[1]) : null,
                            Sumpairs = dr.ItemArray[2] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[2]) : null,
                            SumSortPack = dr.ItemArray[3] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[3]) : null,
                            SumSortPairs = dr.ItemArray[4] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[4]) : null,
                            SumlossPairs = dr.ItemArray[5] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[5]) : null,
                            SumStoragePack = dr.ItemArray[6] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[6]) : null,
                            SumStoragePairs = dr.ItemArray[7] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[7]) : null,
                            MissPackage = dr.ItemArray[8] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[8]) : null,
                            MissPairs = dr.ItemArray[9] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[9]) : null,
                            Proportion = dr.ItemArray[10] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[10]) : null
                        };
                        headers.Add(header);
                    }
                    headers.Add(new ReportCheckSumMissHeader
                    {
                        yymmdd = "合計",
                        Sumpackage = headers.Sum(x => x.Sumpackage),
                        Sumpairs = headers.Sum(x => x.Sumpairs),
                        SumSortPack = headers.Sum(x => x.SumSortPack),
                        SumSortPairs = headers.Sum(x => x.SumSortPairs),
                        SumlossPairs = headers.Sum(x => x.SumlossPairs),
                        SumStoragePack = headers.Sum(x => x.SumStoragePack),
                        SumStoragePairs = headers.Sum(x => x.SumStoragePairs),
                        MissPackage = headers.Sum(x => x.MissPackage),
                        MissPairs = headers.Sum(x => x.MissPairs),
                        Proportion = headers.Sum(x => x.Sumpairs) > 0 ? Math.Round((headers.Sum(x => x.MissPairs).Value / headers.Sum(x => x.Sumpairs)).Value, 2) : 0
                    });
                }

                List<ReportCheckSumMissDetail> details = new();
                foreach (DataRow dr in tableDetail.Rows)
                {
                    ReportCheckSumMissDetail detail = new()
                    {
                        yymmdd = dr.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[0]) : null,
                        Manuf = dr.ItemArray[1] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[1]) : null,
                        Type = dr.ItemArray[2] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[2]) : null,
                        ManNo = dr.ItemArray[3] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[3]) : null,
                        PurNo = dr.ItemArray[4] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[4]) : null,
                        wkshno = dr.ItemArray[5] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[5]) : null,
                        prtno = dr.ItemArray[6] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[6]) : null,
                        QRCodeID = dr.ItemArray[7] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[7]) : null,
                        model = dr.ItemArray[8] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[8]) : null,
                        tolcls = dr.ItemArray[9] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[9]) : null,
                        bitnbr = dr.ItemArray[10] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[10]) : null,
                        eta = dr.ItemArray[11] != (object)DBNull.Value ? Convert.ToDateTime(dr.ItemArray[11]) : null,
                        empno = dr.ItemArray[12] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[12]) : null,
                        serial = dr.ItemArray[13] != (object)DBNull.Value ? Convert.ToInt16(dr.ItemArray[13]) : null,
                        size = dr.ItemArray[14] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[14]) : null,
                        qty = dr.ItemArray[15] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[15]) : null,
                        CrUsr = dr.ItemArray[16] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[16]) : null,
                        crdaY = dr.ItemArray[17] != (object)DBNull.Value ? Convert.ToDateTime(dr.ItemArray[17]) : null,
                        Sortflag = dr.ItemArray[18] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[18]) : null,
                        Storageflag = dr.ItemArray[19] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[19]) : null
                    };
                    details.Add(detail);
                }
                await conn.CloseAsync();

                Report_Check_Sum_Miss results = new()
                {
                    Headers = headers,
                    Details = PaginationUtility<ReportCheckSumMissDetail>.Create(details, pagination.PageNumber, pagination.PageSize, is_Paging)
                };

                return results;
            }
        }
    }
}