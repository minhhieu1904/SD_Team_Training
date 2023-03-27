using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public partial class Report_New4_5_ByMidasResult
    {
        public string cusid { get; set; }
        public DateTime? eta { get; set; }
        public string eta_format { get => eta.HasValue ? eta.Value.ToString("yyyy-MM-dd") : null; }
        public string kind { get; set; }
        public DateTime sdat { get; set; }
        public string sdat_format { get => sdat.ToString("yyyy-MM-dd"); }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string bitnbr { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string article { get; set; }
        public string style { get; set; }
        public string Size { get; set; }
        [Column(TypeName = "decimal(7,1)")]
        public decimal? ORQTY { get; set; }
        [Column(TypeName = "decimal(38,1)")]
        public decimal beforeQTY { get; set; }
        [Column(TypeName = "decimal(38,1)")]
        public decimal? QTY { get; set; }
        [Column(TypeName = "decimal(38,1)")]
        public decimal? remainingQTY { get; set; }
    }
}