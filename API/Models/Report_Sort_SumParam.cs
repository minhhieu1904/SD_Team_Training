

namespace API.Models
{
    public class Report_Sort_SumParam
    {
        public DateTime? CrDay { get; set; }
        public DateTime? mdat { get; set; }
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
        public decimal? sort_qty { get; set; }
        public decimal? diff_qty { get; set; }
        public decimal? cqty { get; set; }
    }
}