using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_QR_Label")]
    public partial class MsQrLabel
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [StringLength(1)]
        public string? Type { get; set; }
        [StringLength(15)]
        public string ManNo { get; set; } = null!;
        [StringLength(10)]
        public string PurNo { get; set; } = null!;
        /// <summary>
        /// 派工單號
        /// </summary>
        [Column("wkshno")]
        [StringLength(10)]
        public string Wkshno { get; set; } = null!;
        /// <summary>
        /// 列印單號
        /// </summary>
        [Column("prtno")]
        [StringLength(10)]
        public string Prtno { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Grade { get; set; } = null!;
        [Key]
        [Column("QRCodeID")]
        [StringLength(13)]
        public string QrcodeId { get; set; } = null!;
        [Column("QRCodeValue")]
        [StringLength(73)]
        public string? QrcodeValue { get; set; }
        [Column("empno")]
        [StringLength(5)]
        public string Empno { get; set; } = null!;
        [StringLength(3)]
        public string? Seq { get; set; }
        [StringLength(21)]
        public string? CyNo { get; set; }
        public short Serial { get; set; }
        [StringLength(4)]
        public string Size { get; set; } = null!;
        [Column(TypeName = "decimal(7, 1)")]
        public decimal Qty { get; set; }
        [Column("Prt_Cnt")]
        public short PrtCnt { get; set; }
        [StringLength(1)]
        public string Flag { get; set; } = null!;
        [StringLength(6)]
        public string CrUsr { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime CrDay { get; set; }
        [StringLength(6)]
        public string? CanUsr { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CanDay { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime PrtDay { get; set; }
        [StringLength(100)]
        public string? RptReason { get; set; }
        [StringLength(6)]
        public string? RptUsr { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? RptDate { get; set; }
    }
}
