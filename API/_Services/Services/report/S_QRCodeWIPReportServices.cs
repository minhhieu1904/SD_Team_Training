using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.report;
using API.DTOs.report.QRCodeWIPReport;
using Aspose.Cells;
using Microsoft.Data.SqlClient;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.report
{
    public class S_QRCodeWIPReportServices : I_QRCodeWIPReportServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        private string area;
        public S_QRCodeWIPReportServices(
            IRepositoryAccessor repositoryAccessor, 
            IWebHostEnvironment environment,
            IConfiguration configuration
            ){
                _repositoryAccessor = repositoryAccessor;
                _environment = environment;
                _configuration = configuration;
                area = _configuration.GetSection("Appsettings:Area").Value;
            }
        public async Task<Tuple<List<CheckSumMissHeaderDTO>, List<CheckSumMissDetailDTO>>> GetData(QRCodeParam param)
        {
            List<CheckSumMissHeaderDTO> listHeader = new List<CheckSumMissHeaderDTO>();
            List<CheckSumMissDetailDTO> listDetail = new List<CheckSumMissDetailDTO>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString($"DefaultConnection_{area}")))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("CheckSumMiss", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("@moldno", SqlDbType.NChar).Value = param.rmodel ?? (object)DBNull.Value;
                cmd.Parameters.Add("@toolno", SqlDbType.NChar).Value = param.tolcls ?? (object)DBNull.Value;
                cmd.Parameters.Add("mdat_start", SqlDbType.DateTime).Value = param.mdat_start ?? (object)DBNull.Value;
                cmd.Parameters.Add("mdat_end", SqlDbType.DateTime).Value = param.mdat_end ?? (object)DBNull.Value;
                await cmd.ExecuteNonQueryAsync();
                
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable tableHeader = ds.Tables[0];
                DataTable tableDetail = ds.Tables[1];

                foreach (DataRow dr in tableHeader.Rows)
                {
                    CheckSumMissHeaderDTO header = new CheckSumMissHeaderDTO()
                    {
                        yymmdd = dr.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[0]) : null,
                        Sumpairs = dr.ItemArray[1] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[1]) : null,
                        Sumpackage = dr.ItemArray[1] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[2]) : null,
                        SumSortPack = dr.ItemArray[3] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[3]) : null,
                        SumSortPairs = dr.ItemArray[4] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[4]) : null,
                        SumlossPairs = dr.ItemArray[5] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[5]) : null,
                        SumStoragePack = dr.ItemArray[6] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[6]) : null,
                        SumStoragePairs = dr.ItemArray[7] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[7]) : null,
                        MissPackage = dr.ItemArray[8] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[8]) : null,
                        MissPairs = dr.ItemArray[9] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[9]) : null,
                        Proportion = dr.ItemArray[10] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[10]) : null
                    };
                    listHeader.Add(header);
                }

                listHeader.Add(new CheckSumMissHeaderDTO()
                {
                    yymmdd = "合計",
                    Sumpackage = listHeader.Sum(x => x.Sumpackage),
                    Sumpairs = listHeader.Sum(x => x.Sumpairs),
                    SumSortPack = listHeader.Sum(x => x.SumSortPack),
                    SumSortPairs = listHeader.Sum(x => x.SumSortPairs),
                    SumlossPairs = listHeader.Sum(x => x.SumlossPairs),
                    SumStoragePack = listHeader.Sum(x => x.SumStoragePack),
                    SumStoragePairs = listHeader.Sum(x => x.SumStoragePairs),
                    MissPackage = listHeader.Sum(x => x.MissPackage),
                    MissPairs = listHeader.Sum(x => x.MissPairs),
                    Proportion = listHeader.Sum(x => x.Proportion) > 0 ? Math.Round(listHeader.Sum(x => x.MissPairs).Value / listHeader.Sum(x => x.Sumpairs).Value, 2) : 0
                });

                foreach (DataRow dr in tableDetail.Rows)
                {

                    CheckSumMissDetailDTO detail = new CheckSumMissDetailDTO()
                    {
                        yymmdd = dr.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[0]) : null,
                        Manuf = dr.ItemArray[1] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[1]) : null,
                        Type = dr.ItemArray[2] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[2]) : null,
                        Manno = dr.ItemArray[3] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[3]) : null,
                        Purno = dr.ItemArray[4] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[4]) : null,
                        prtno = dr.ItemArray[5] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[5]) : null,
                        wkshno = dr.ItemArray[6] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[6]) : null,
                        QRCodeID = dr.ItemArray[7] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[7]) : null,
                        rmodel = dr.ItemArray[8] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[8]) : null,
                        tolcls = dr.ItemArray[9] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[9]) : null,
                        bitnbr = dr.ItemArray[10] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[10]) : null,
                        eta = dr.ItemArray[11] != (object)DBNull.Value ? Convert.ToDateTime(dr.ItemArray[11]) : null,
                        empno = dr.ItemArray[12] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[12]) : null,
                        serial = dr.ItemArray[13] != (object)DBNull.Value ? Convert.ToInt16(dr.ItemArray[13]) : null,
                        size = dr.ItemArray[14] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[14]) : null,
                        qty = dr.ItemArray[15] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[15]) : null,
                        Crusr = dr.ItemArray[16] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[16]) : null,
                        Crday = dr.ItemArray[17] != (object)DBNull.Value ? Convert.ToDateTime(dr.ItemArray[17]) : null,
                        softFlag = dr.ItemArray[18] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[18]) : null,
                        storageFlag = dr.ItemArray[19] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[19]) : null
                    };
                    listDetail.Add(detail);
                }
            }
            return Tuple.Create(listHeader, listDetail);
        }
public async Task<byte[]> ExportExcel(QRCodeParam param)
        {
            Tuple<List<CheckSumMissHeaderDTO>, List<CheckSumMissDetailDTO>> data = await GetData(param);

            MemoryStream stream = new MemoryStream();
            if (data.Item1.Any() && data.Item2.Any())
            {
                var path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\CheckSumMissReport.xlsx");

                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws1 = designer.Workbook.Worksheets[0];
                Worksheet ws2 = designer.Workbook.Worksheets[1];
                ws1.Cells["I2"].PutValue(DateTime.Now);

                foreach(var dr in data.Item2){
                    dr.yymmdd = DateTime.ParseExact(Convert.ToString(dr.yymmdd), "yyMMdd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                }
                designer.SetDataSource("header", data.Item1);
                designer.SetDataSource("detail", data.Item2);
                designer.Process();
                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            return stream.ToArray();
        }

        public async Task<PaginationUtility<CheckSumMissDetailDTO>> GetDataPagination(PaginationParam param, QRCodeParam model, bool isPaing = true)
        {
            Tuple<List<CheckSumMissHeaderDTO>, List<CheckSumMissDetailDTO>> data = await GetData(model);
            return PaginationUtility<CheckSumMissDetailDTO>.Create(data.Item2, param.PageNumber, param.PageSize, isPaing);
        }
    }
}