using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_QR_Sort")]
    public partial class MsQrSort
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [Column("SDat", TypeName = "date")]
        public DateTime Sdat { get; set; }
        [Key]
        [StringLength(12)]
        public string TrNo { get; set; } = null!;
        [StringLength(1)]
        public string Shift { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Grade { get; set; } = null!;
        [Key]
        [Column("QRCodeID")]
        [StringLength(13)]
        public string QrcodeId { get; set; } = null!;
        [StringLength(15)]
        public string ManNo { get; set; } = null!;
        [StringLength(15)]
        public string PurNo { get; set; } = null!;
        public short Serial { get; set; }
        [StringLength(4)]
        public string Size { get; set; } = null!;
        [Column(TypeName = "decimal(7, 1)")]
        public decimal Qty { get; set; }
        [StringLength(6)]
        public string CrUsr { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime CrDay { get; set; }
        [StringLength(1)]
        public string EndCod { get; set; } = null!;
    }
}
