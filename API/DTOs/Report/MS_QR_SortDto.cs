
namespace API.DTOs.Report
{
    public class MSQRSortDto
    {
        public string Manuf { get; set; }
        public DateTime Sdat { get; set; }
        public string TrNo { get; set; }
        public string Shift { get; set; }
        public string Grade { get; set; }
        public string QrcodeId { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public short Serial { get; set; }
        public string Size { get; set; }
        public decimal Qty { get; set; }
        public string CrUsr { get; set; }
        public DateTime CrDay { get; set; }
        public string EndCod { get; set; }
        public bool Check { get; set; }
    }

    public class ReportSortSum
    {
        public DateTime? date_kind { get; set; }
        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? etd_start { get; set; }
        public DateTime? etd_end { get; set; }
        public string size { get; set; }
    }

    public class ReportSortSumExcelDetail
    {
        public string IsScanSort { get; set; }
        public string QrcodeId { get; set; }
        public DateTime? CrDay { get; set; }
        public string Brandname { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short Serial { get; set; }
        public decimal Qty { get; set; }
        public string Cusna { get; set; }
        public string Rmodel { get; set; }
        public string Tolcls { get; set; }
        public string Ritnbr { get; set; }
        public string Bitnbr { get; set; }
        public string Article { get; set; }
        public string Kind { get; set; }
        public DateTime? Eta { get; set; }
    }

}