
namespace API.DTOs
{
    public class WkshSumReportParam
    {
        public DateTime? mdat_start { get; set; }
        public DateTime? mdat_end { get; set; }
        public string close_status { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string purno { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? eta_start { get; set; }
        public DateTime? eta_end { get; set; }
        public string size { get; set; }
    }
}