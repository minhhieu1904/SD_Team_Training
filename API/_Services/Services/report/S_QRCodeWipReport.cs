using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces.report;
using API.DTOs.Report;
using Aspose.Cells;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.report
{
    public class S_QRCodeWipReport : I_QRCodeWipReport
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        private readonly IWebHostEnvironment _environment;

        private readonly IConfiguration _configuration;
        private string area;
        public S_QRCodeWipReport(IRepositoryAccessor repositoryAccessor, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _repositoryAccessor = repositoryAccessor;
            _environment = environment;
            _configuration = configuration;
            area = _configuration.GetSection("Appsettings:Area").Value;
        }

        public async Task<byte[]> ExportExcel(CheckSumMissReportParam param)
        {
            Tuple<List<CheckSumMiss_TB1>, List<CheckSumMiss_TB2>> data = await GetData(param);

            MemoryStream stream = new MemoryStream();
            if (data.Item1.Count > 0 && data.Item2.Count > 0)
            {
                var path = Path.Combine(_environment.ContentRootPath, "Resources\\Template\\QRCodeWipReport\\CheckSumMissReport.xlsx");

                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);
                Worksheet ws1 = designer.Workbook.Worksheets[0];
                Worksheet ws2 = designer.Workbook.Worksheets[1];
                ws1.Cells["I2"].PutValue(DateTime.Now);

                foreach(var dr in data.Item2){
                    dr.Yymmdd = DateTime.ParseExact(Convert.ToString(dr.Yymmdd), "yyMMdd", CultureInfo.InvariantCulture).ToString("dd/MM/yyyy");
                }
                designer.SetDataSource("header", data.Item1);
                designer.SetDataSource("detail", data.Item2);
                designer.Process();
                ws2.AutoFitColumns();
                ws2.PageSetup.CenterHorizontally = true;
                ws2.PageSetup.FitToPagesWide = 1;
                ws2.PageSetup.FitToPagesTall = 0;

                designer.Workbook.Save(stream, SaveFormat.Xlsx);
            }
            return stream.ToArray();
        }

        public async Task<Tuple<List<CheckSumMiss_TB1>, List<CheckSumMiss_TB2>>> GetData(CheckSumMissReportParam param)
        {
            List<CheckSumMiss_TB2> l_tb2 = new List<CheckSumMiss_TB2>();
            List<CheckSumMiss_TB1> l_tb1 = new List<CheckSumMiss_TB1>();
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString($"DefaultConnection_{area}")))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("CheckSumMiss", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600; // 3600 giây = 10 phút
                cmd.Parameters.Add("@moldno", SqlDbType.NVarChar).Value = param.rmodel ?? (object)DBNull.Value;
                cmd.Parameters.Add("@toolno", SqlDbType.NVarChar).Value = param.tolcls ?? (object)DBNull.Value;
                cmd.Parameters.Add("@mdat_start", SqlDbType.Date).Value = param.mdat_start ?? (object)DBNull.Value;
                cmd.Parameters.Add("@mdat_end", SqlDbType.Date).Value = param.mdat_end ?? (object)DBNull.Value;
                await cmd.ExecuteNonQueryAsync();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DataTable tb1 = ds.Tables[0];
                DataTable tb2 = ds.Tables[1];

                string[] format = {"yyyyMMdd"};
                DateTime date;

                foreach(DataRow dr in tb1.Rows){
                    CheckSumMiss_TB1 tb = new CheckSumMiss_TB1(){
                        Yymmdd = dr.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[0]) : null,
                        SumPackage = dr.ItemArray[1] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[1]) : null,
                        SumPairs = dr.ItemArray[2] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[2]) : null,
                        SumSortPack = dr.ItemArray[3] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[3]): null,
                        SumSortPairs = dr.ItemArray[4] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[4]) : null,
                        SumLossPairs = dr.ItemArray[5] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[5]) : null,
                        SumStoragePack = dr.ItemArray[6] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[6]) : null,
                        SumStoragePairs = dr.ItemArray[7] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[7]) : null,
                        MissPackage = dr.ItemArray[8] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[8]) : null,
                        MissPairs = dr.ItemArray[9] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[9]) : null,
                        ProPortion = dr.ItemArray[10] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[10]) : null,
                    };
                    l_tb1.Add(tb);
                }
                l_tb1.Add(new CheckSumMiss_TB1()
                {
                    Yymmdd = "合計",
                    SumPackage = l_tb1.Sum(x => x.SumPackage),
                    SumPairs = l_tb1.Sum(x => x.SumPairs),
                    SumSortPack = l_tb1.Sum(x => x.SumSortPack),
                    SumSortPairs = l_tb1.Sum(x => x.SumSortPairs),
                    SumLossPairs = l_tb1.Sum(x => x.SumLossPairs),
                    SumStoragePack = l_tb1.Sum(x => x.SumStoragePack),
                    SumStoragePairs = l_tb1.Sum(x => x.SumStoragePairs),
                    MissPackage = l_tb1.Sum(x => x.MissPackage),
                    MissPairs = l_tb1.Sum(x => x.MissPairs),
                    ProPortion = l_tb1.Sum(x => x.SumPairs) > 0 ? Math.Round(l_tb1.Sum(x => x.MissPairs).Value / l_tb1.Sum(x => x.SumPairs).Value, 2) : 0
                });
                foreach (DataRow dr in tb2.Rows)
                {
                    
                    CheckSumMiss_TB2 tb = new CheckSumMiss_TB2()
                    {
                        
                        Yymmdd = dr.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[0]) : null,
                        Manuf = dr.ItemArray[1] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[1]) : null,
                        Type = dr.ItemArray[2] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[2]) : null,
                        ManNo = dr.ItemArray[3] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[3]) : null,
                        PurNo = dr.ItemArray[4] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[4]) : null,
                        PrtNo = dr.ItemArray[5] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[5]) : null,
                        WkshNo = dr.ItemArray[6] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[6]) : null,
                        QRCodeID = dr.ItemArray[7] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[7]) : null,
                        Rmodel = dr.ItemArray[8] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[8]) : null,
                        Tolcls = dr.ItemArray[9] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[9]) : null,
                        Bitnbr = dr.ItemArray[10] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[10]) : null,
                        Eta = dr.ItemArray[11] != (object)DBNull.Value ? Convert.ToDateTime(dr.ItemArray[11]) : null,
                        EmpNo = dr.ItemArray[12] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[12]) : null,
                        Serial = dr.ItemArray[13] != (object)DBNull.Value ? Convert.ToInt16(dr.ItemArray[13]) : null,
                        Size = dr.ItemArray[14] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[14]) : null,
                        Qty = dr.ItemArray[15] != (object)DBNull.Value ? Convert.ToDecimal(dr.ItemArray[15]) : null,
                        CrUsr = dr.ItemArray[16] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[16]) : null,
                        CrdaY = dr.ItemArray[17] != (object)DBNull.Value ? Convert.ToDateTime(dr.ItemArray[17]) : null,
                        SortFlag = dr.ItemArray[18] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[18]) : null,
                        StorageFlag = dr.ItemArray[19] != (object)DBNull.Value ? Convert.ToString(dr.ItemArray[19]) : null,
                    };
                    l_tb2.Add(tb);
                }

            }
            return Tuple.Create(l_tb1, l_tb2);
        }

        public async Task<PaginationUtility<CheckSumMiss_TB2>> GetDataPagination([FromQuery] PaginationParam pagination, CheckSumMissReportParam param)
        {
            Tuple<List<CheckSumMiss_TB1>, List<CheckSumMiss_TB2>> data = await GetData(param);
            return PaginationUtility<CheckSumMiss_TB2>.Create(data.Item2,pagination.PageNumber,pagination.PageSize);

        }
    }
}