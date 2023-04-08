

namespace API.DTOs.Report
{
    public class SortSumDTO
    {
        public DateTime? CrDay { get; set; }
        public string date_kind{ get; set; }
        public DateTime?date_start{ get; set; }
        public DateTime?date_end{ get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? eta_start { get; set; }
        public DateTime? eta_end { get; set; }
        public string size { get; set; }
    }

    public class SortSumDeltailDTO
    {
        public string IsScanSort{ get; set; }
        public DateTime? CrDay { get; set; }
        public string brandname { get; set; }
        public string QRCodeID { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
        public short Serial { get; set; }
        public decimal label_Qty { get; set; }
        public string cusna { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string ritnbr { get; set; }
        public string bitnbr { get; set; }
        public string article { get; set; }
        public string kind { get; set; }
        public DateTime? eta { get; set; }
    }
       public class SortSumDeltailDTOParam
    { 
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
    }
}