using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_QR_PickingDetail")]
    public partial class MsQrPickingDetail
    {
        [Key]
        [Column("manuf")]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [Column(TypeName = "date")]
        public DateTime Sdat { get; set; }
        [Column("iodat", TypeName = "date")]
        public DateTime? Iodat { get; set; }
        [Key]
        [Column("pickingTrNo")]
        [StringLength(12)]
        public string PickingTrNo { get; set; } = null!;
        [Column("storageTrNo")]
        [StringLength(12)]
        public string StorageTrNo { get; set; } = null!;
        [StringLength(1)]
        [Unicode(false)]
        public string Grade { get; set; } = null!;
        [Key]
        [Column("QRCodeID")]
        [StringLength(13)]
        public string QrcodeId { get; set; } = null!;
        [Column("manno")]
        [StringLength(15)]
        public string Manno { get; set; } = null!;
        [Column("purno")]
        [StringLength(10)]
        public string Purno { get; set; } = null!;
        [Column("serial", TypeName = "decimal(5, 0)")]
        public decimal Serial { get; set; }
        [Column("size")]
        [StringLength(4)]
        public string Size { get; set; } = null!;
        [Column("qty", TypeName = "decimal(7, 1)")]
        public decimal Qty { get; set; }
        [Column("crusr")]
        [StringLength(6)]
        public string? Crusr { get; set; }
        [Column("crday", TypeName = "datetime")]
        public DateTime? Crday { get; set; }
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
        [Column("wkshqty", TypeName = "decimal(7, 1)")]
        public decimal Wkshqty { get; set; }
        [Column("mdat", TypeName = "date")]
        public DateTime Mdat { get; set; }
        [Column("empno")]
        [StringLength(5)]
        public string Empno { get; set; } = null!;
        /// <summary>
        /// 品號
        /// </summary>
        [Column("bitnbr")]
        [StringLength(16)]
        public string Bitnbr { get; set; } = null!;
        /// <summary>
        /// 鞋廠料號
        /// </summary>
        [Column("ritnbr")]
        [StringLength(10)]
        public string Ritnbr { get; set; } = null!;
    }
}
