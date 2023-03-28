using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_QR_PickingMain")]
    public partial class MsQrPickingMain
    {
        [Key]
        [Column("manuf")]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [Key]
        [Column("pickingTrNo")]
        [StringLength(12)]
        public string PickingTrNo { get; set; } = null!;
        [Column("storeh")]
        [StringLength(2)]
        public string Storeh { get; set; } = null!;
        [Column("parno")]
        [StringLength(3)]
        public string Parno { get; set; } = null!;
        [Column("crusr")]
        [StringLength(6)]
        public string? Crusr { get; set; }
        [Column("crday", TypeName = "datetime")]
        public DateTime? Crday { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }
        [Column("status")]
        [StringLength(10)]
        public string Status { get; set; } = null!;
    }
}
