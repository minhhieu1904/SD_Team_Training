
namespace API.DTOs.Report
{
    public class StorageSumReportDTO
    {
        public DateTime? crday { get; set; }

        public DateTime mdat { get; set; }
        public string brandname { get; set; }
        public string cusid { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string ritnbr { get; set; }
        public string bitnbr { get; set; }
        public string article { get; set; }
        public string kind { get; set; }
        public DateTime? eta { get; set; }
        public string size { get; set; }
        public decimal? qty { get; set; }
        public decimal? wkshqty { get; set; }
        public decimal? pqty { get; set; }
        public decimal sort_qty { get; set; }
        public decimal storage_qty { get; set; }
        public decimal? diff_qty { get; set; }
        public decimal? cqty { get; set; }
    }

    public class StorageSumDetailDTO
    {
        public string IsScanSort { get; set; }
        public string CrDay { get; set; }
        public string BrandName { get; set; }
        public string QRCodeID { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short Serial { get; set; }
        public decimal Qty { get; set; }
        public string Cusid { get; set; }
        public string Cusna { get; set; }
        public string Rmodel { get; set; }
        public string? Tolcls { get; set; }
        public string Ritnbr { get; set; }
        public string Bitnbr { get; set; }
        public string Article { get; set; }
        public string Kind { get; set; }
        public string Eta { get; set; }
    }
}