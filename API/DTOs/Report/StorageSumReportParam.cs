

namespace API.DTOs.Report
{
    public class StorageSumReportParam
    {
        public string date_kind { get; set; }
        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        public string brandname { get; set; }
        public string cusid { get; set; }
        public string manno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string purno { get; set; }
        public string material { get; set; }
        public string kind { get; set; }
        public DateTime? etd_start { get; set; }
        public DateTime? etd_end { get; set; }
        public string size { get; set; }
    }

    public class StorageSumDetailReportParam
    {
        public string manno { get; set; }

        public string purno { get; set; }

        public string size { get; set; }
    }
}