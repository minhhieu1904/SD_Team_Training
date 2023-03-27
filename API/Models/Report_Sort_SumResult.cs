
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public partial class Report_Sort_SumResult
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
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? wkshqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? pqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal sort_qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? diff_qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? cqty { get; set; }
    }
}