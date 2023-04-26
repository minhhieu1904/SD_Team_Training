using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{

    public partial class MS_QR_Cycle
    {
        [Key]
        [StringLength(1)]
        public string manuf { get; set; }
        [Key]
        [StringLength(15)]
        public string manno { get; set; }
        [Key]
        [StringLength(3)]
        public string seq { get; set; }
        [Key]
        [StringLength(10)]
        public string purno { get; set; }
        [Required]
        [StringLength(6)]
        public string article { get; set; }
        [Key]
        [StringLength(21)]
        public string cyno { get; set; }
        [Key]
        [StringLength(4)]
        public string size { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal qty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal pqty { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal cqty { get; set; }
        [Required]
        [StringLength(1)]
        public string endcod { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime update_time { get; set; }
        [StringLength(15)]
        public string updated_by { get; set; }
        [StringLength(54)]
        public string Biz_Key { get; set; }
    }
}
