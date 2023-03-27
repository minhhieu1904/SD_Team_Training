
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Dtos.Commons
{
    public class MS_QR_StorageDto
    {
        [Key]
        [StringLength(1)]
        public string manuf { get; set; }
        [Column(TypeName = "date")]
        public DateTime Sdat { get; set; }
        [Column(TypeName = "date")]
        public DateTime iodat { get; set; }
        [Key]
        [StringLength(12)]
        public string trno { get; set; }
        [Required]
        [StringLength(2)]
        public string storeh { get; set; }
        [Required]
        [StringLength(3)]
        public string parno { get; set; }
        [Key]
        [StringLength(15)]
        public string manno { get; set; }
        [Key]
        [StringLength(10)]
        public string purno { get; set; }
        [Key]
        [Column(TypeName = "decimal(5, 0)")]
        public decimal serial { get; set; }
        [Required]
        [StringLength(4)]
        public string size { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal qty { get; set; }
        [Required]
        [StringLength(6)]
        public string crusr { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime crday { get; set; }
        [Required]
        [StringLength(1)]
        public string biz_flag { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? biz_time { get; set; }

    }
}
