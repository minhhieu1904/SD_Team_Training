
using System.Data;
using System.Globalization;

using API._Repositories;
using API._Services.Interfaces.Report;
using API.Data;
using API.DTOs.Report;
using Aspose.Cells;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Report
{
    public class Report_QRCODE_WIPservices : IReport_QRCODE_WIPservices
    {

        private readonly IRepositoryAccessor _reposioryAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private DBContext _dbContext;
        private readonly IConfiguration _configuration;
        private string area;

        public Report_QRCODE_WIPservices(IRepositoryAccessor reposioryAccessor, IWebHostEnvironment webHostEnvironment, DBContext dbContext, IConfiguration configuration)
        {
            _reposioryAccessor = reposioryAccessor;
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
            _configuration = configuration;
            area = _configuration.GetSection("Appsettings:Area").Value;

        }
        public async Task<PaginationUtility<QRCODEWIPDetailReportDTO>> GetDataPagination(PaginationParam pagination, ReportQRCODEWIPParam param)
        {
            Tuple<List<QRCODEWIPHeaderReportDTO>, List<QRCODEWIPDetailReportDTO>> data = await GetData(param);           
            return PaginationUtility<QRCODEWIPDetailReportDTO>.Create(data.Item2, pagination.PageNumber, pagination.PageSize);  

        }
        public async Task<Tuple<List<QRCODEWIPHeaderReportDTO>, List<QRCODEWIPDetailReportDTO>>> GetData(ReportQRCODEWIPParam param)
        {
            List<QRCODEWIPDetailReportDTO> listDetail = new List<QRCODEWIPDetailReportDTO>();
            List<QRCODEWIPHeaderReportDTO> listHeader = new List<QRCODEWIPHeaderReportDTO>();
            using (SqlConnection conn = new SqlConnection (_configuration.GetConnectionString($"DefaultConnection_{area}")))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("CheckSumMiss", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = 3600;
                cmd.Parameters.Add("@moldno",SqlDbType.NVarChar).Value = param.rmodel ?? (object)DBNull.Value;
                cmd.Parameters.Add("@toolno",SqlDbType.NVarChar).Value = param.tolcls ?? (object)DBNull.Value;
                cmd.Parameters.Add("@mdat_start",SqlDbType.DateTime).Value = param.mdat_start ?? (object)DBNull.Value;
                cmd.Parameters.Add("@mdat_end",SqlDbType.DateTime).Value = param.mdat_end ?? (object)DBNull.Value;
                await cmd.ExecuteNonQueryAsync();
                SqlDataAdapter  adapter = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                adapter.Fill(ds);
                DataTable tableHeader = ds.Tables[0];
                DataTable tableDetail = ds.Tables[1];
                foreach(DataRow drow in tableHeader.Rows)
                {
                    QRCODEWIPHeaderReportDTO header = new QRCODEWIPHeaderReportDTO()
                    {
                        Yymmdd = drow.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[0]) : null,
                        SumPackage = drow.ItemArray[1] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[1]) : null,
                        SumPairs = drow.ItemArray[2] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[2]) : null,
                        SumSortPack = drow.ItemArray[3] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[3]) : null,
                        SumSortPairs = drow.ItemArray[4] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[4]) : null,
                        SumLossPairs = drow.ItemArray[5] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[5]) : null,
                        SumStoragePack = drow.ItemArray[6] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[6]) : null,
                        SumStoragePairs = drow.ItemArray[7] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[7]) : null,
                        MissPackage = drow.ItemArray[8] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[8]) : null,
                        MissPairs = drow.ItemArray[9] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[9]) : null,
                        ProPortion = drow.ItemArray[10] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[10]) : null,
                    };
                    listHeader.Add(header);
                }
                  listHeader.Add(new QRCODEWIPHeaderReportDTO()
                    {
                        Yymmdd = "合計",
                        SumPackage = listHeader.Sum(x => x.SumPackage),
                        SumPairs = listHeader.Sum(x => x.SumPairs),
                        SumSortPack = listHeader.Sum(x => x.SumSortPack),
                        SumSortPairs = listHeader.Sum(x => x.SumSortPairs),
                        SumLossPairs = listHeader.Sum(x => x.SumLossPairs),
                        SumStoragePack = listHeader.Sum(x => x.SumStoragePack),
                        SumStoragePairs = listHeader.Sum(x => x.SumStoragePairs),
                        MissPackage = listHeader.Sum(x => x.MissPackage),
                        MissPairs = listHeader.Sum(x => x.MissPairs),
                        ProPortion = listHeader.Sum(x => x.SumPairs) > 0 ? 
                        Math.Round(listHeader.Sum(x => x.MissPairs).Value / listHeader.Sum(x => x.SumPairs).Value, 2) : 0
                    });
                
                foreach (DataRow drow in tableDetail.Rows)
                {   
                    QRCODEWIPDetailReportDTO detail = new QRCODEWIPDetailReportDTO()
                    {
                        Yymmdd = drow.ItemArray[0] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[0]) : null,
                        Manuf = drow.ItemArray[1] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[1]) : null,
                        Type = drow.ItemArray[2] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[2]) : null,
                        ManNo = drow.ItemArray[3] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[3]) : null,
                        PurNo = drow.ItemArray[4] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[4]) : null,
                        PrtNo = drow.ItemArray[5] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[5]) : null,
                        WkshNo = drow.ItemArray[6] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[6]) : null,
                        QRCodeID = drow.ItemArray[7] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[7]) : null,
                        Rmodel = drow.ItemArray[8] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[8]) : null,
                        Tolcls = drow.ItemArray[9] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[9]) : null,
                        Bitnbr = drow.ItemArray[10] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[10]) : null,
                        Eta = drow.ItemArray[11] != (object)DBNull.Value ? Convert.ToDateTime(drow.ItemArray[11]) : null,
                        EmpNo = drow.ItemArray[12] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[12]) : null,
                        Serial = drow.ItemArray[13] != (object)DBNull.Value ? Convert.ToInt16(drow.ItemArray[13]) : null,
                        Size = drow.ItemArray[14] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[14]) : null,
                        Qty = drow.ItemArray[15] != (object)DBNull.Value ? Convert.ToDecimal(drow.ItemArray[15]) : null,
                        CrUsr = drow.ItemArray[16] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[16]) : null,
                        CrdaY = drow.ItemArray[17] != (object)DBNull.Value ? Convert.ToDateTime(drow.ItemArray[17]) : null,
                        CrdaY_str = drow.ItemArray[17] != (object)DBNull.Value ? Convert.ToDateTime(drow.ItemArray[17])
                                            .ToString("yyyy/M/d tt h:mm:ss", CultureInfo.GetCultureInfo("zh-TW")) : null,
                        SortFlag = drow.ItemArray[18] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[18]) : null,
                        StorageFlag = drow.ItemArray[19] != (object)DBNull.Value ? Convert.ToString(drow.ItemArray[19]) : null
                    };   listDetail.Add(detail);
                }
            };
            return Tuple.Create(listHeader, listDetail);
        }

        public async Task<byte[]> ExportExcel(ReportQRCODEWIPParam param)
        {
            Tuple<List<QRCODEWIPHeaderReportDTO>, List<QRCODEWIPDetailReportDTO>> data = await GetData(param);
            MemoryStream stream = new MemoryStream();
            if(data.Item1.Any() && data.Item2.Any())
            {
                var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Resources\\Template\\Report\\4.4Report_QRCODE_WIP\\Report_QRCODE_WIP.xlsx");
                WorkbookDesigner designer = new WorkbookDesigner();
                designer.Workbook = new Workbook(path);

                Worksheet ws1 = designer.Workbook.Worksheets[0];
                Worksheet ws2 = designer.Workbook.Worksheets[1];
                ws1.Cells["I2"].PutValue(DateTime.Now);

                designer.SetDataSource("header", data.Item1);
                designer.SetDataSource("detail", data.Item2);
               
                designer.Process();
                ws2.AutoFitColumns();
                ws2.PageSetup.CenterHorizontally = true;
                ws2.PageSetup.FitToPagesWide = 1;
                ws2.PageSetup.FitToPagesTall = 0;
                designer.Workbook.Save(stream,SaveFormat.Xlsx);
            }
            return stream.ToArray();
        }
    }
}