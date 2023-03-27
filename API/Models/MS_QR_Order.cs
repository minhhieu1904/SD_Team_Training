
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public partial class MS_QR_Order
    {
        [Key]
        [StringLength(1)]
        public string manuf { get; set; }
        [Required]
        [StringLength(10)]
        public string brandname { get; set; }
        [Required]
        [StringLength(40)]
        public string cusid { get; set; }
        [Required]
        [StringLength(10)]
        public string cusna { get; set; }
        [Required]
        [StringLength(1)]
        public string kind { get; set; }
        [Key]
        [StringLength(10)]
        public string purno { get; set; }
        [Key]
        [StringLength(15)]
        public string manno { get; set; }
        [Column(TypeName = "date")]
        public DateTime? eta { get; set; }
        [Required]
        [StringLength(16)]
        public string bitnbr { get; set; }
        [Required]
        [StringLength(10)]
        public string ritnbr { get; set; }
        [Required]
        [StringLength(15)]
        public string rmodel { get; set; }
        [StringLength(3)]
        public string tolcls { get; set; }
        [Required]
        [StringLength(6)]
        public string article { get; set; }
        [StringLength(15)]
        public string style { get; set; }
        [Key]
        [StringLength(4)]
        public string size { get; set; }
        [Required]
        [StringLength(4)]
        public string tsize { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal pqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal cqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal addqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal lessqty { get; set; }
        [Required]
        [StringLength(1)]
        public string endcod { get; set; }
        [Required]
        [StringLength(1)]
        public string cycle_flag { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime update_time { get; set; }
        [StringLength(15)]
        public string updated_by { get; set; }
        // Auto generate in DB, using NotMapped to don't update this
        [Required]
        [StringLength(30)]
        [NotMapped]
        public string Biz_Key { get; set; }
        [Key]
        [StringLength(10)]
        public string wkshno { get; set; }
        [Key]
        [StringLength(10)]
        public string prtno { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal wkshqty { get; set; }
        [Column(TypeName = "date")]
        public DateTime mdat { get; set; }
        [StringLength(1)]
        public string uscod { get; set; }
        [StringLength(20)]
        public string remark { get; set; }
    }
}
