using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs.WkshSumReportDto
{
    public class WkshSumReportDto
    {
        public DateTime mdat { get; set; }
        public string is_close { get; set; }
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
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? wkshqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? pqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal sort_qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal storage_qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? diff_qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? cqty { get; set; }
    }

    public class WkshSumReportParam
    {
        public DateTime? mdat_start { get; set; }
        public DateTime? mdat_end { get; set; }
        public string close_status { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? eta_start { get; set; }
        public DateTime? eta_end { get; set; }
        public string size { get; set; }
        public string tolcls { get; set; }
    }
}