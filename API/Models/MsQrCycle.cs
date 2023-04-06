using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// 回轉包裝明細檔
    /// </summary>
    [Table("MS_QR_Cycle")]
    public partial class MsQrCycle
    {
        /// <summary>
        /// 廠別
        /// </summary>
        [Key]
        [Column("manuf")]
        [StringLength(1)]
        [Unicode(false)]
        public string Manuf { get; set; } = null!;
        /// <summary>
        /// 企劃單號
        /// </summary>
        [Key]
        [Column("manno")]
        [StringLength(15)]
        [Unicode(false)]
        public string Manno { get; set; } = null!;
        /// <summary>
        /// 批次
        /// </summary>
        [Key]
        [Column("seq")]
        [StringLength(3)]
        [Unicode(false)]
        public string Seq { get; set; } = null!;
        /// <summary>
        /// 訂購單號
        /// </summary>
        [Key]
        [Column("purno")]
        [StringLength(10)]
        [Unicode(false)]
        public string Purno { get; set; } = null!;
        /// <summary>
        /// 鞋型
        /// </summary>
        [Column("article")]
        [StringLength(6)]
        [Unicode(false)]
        public string Article { get; set; } = null!;
        /// <summary>
        /// 迴轉
        /// </summary>
        [Key]
        [Column("cyno")]
        [StringLength(21)]
        [Unicode(false)]
        public string Cyno { get; set; } = null!;
        /// <summary>
        /// 尺寸
        /// </summary>
        [Key]
        [Column("size")]
        [StringLength(4)]
        [Unicode(false)]
        public string Size { get; set; } = null!;
        /// <summary>
        /// 雙數
        /// </summary>
        [Column("qty", TypeName = "decimal(7, 1)")]
        public decimal Qty { get; set; }
        /// <summary>
        /// 已列印雙數
        /// </summary>
        [Column("pqty", TypeName = "decimal(7, 1)")]
        public decimal Pqty { get; set; }
        /// <summary>
        /// 取消列印雙數
        /// </summary>
        [Column("cqty", TypeName = "decimal(7, 1)")]
        public decimal Cqty { get; set; }
        /// <summary>
        /// 完成列印否
        /// </summary>
        [Column("endcod")]
        [StringLength(1)]
        [Unicode(false)]
        public string Endcod { get; set; } = null!;
        /// <summary>
        /// 異動時間
        /// </summary>
        [Column("update_time", TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 異動者
        /// </summary>
        [Column("updated_by")]
        [StringLength(15)]
        [Unicode(false)]
        public string? UpdatedBy { get; set; }
        [Column("Biz_Key")]
        [StringLength(54)]
        [Unicode(false)]
        public string? BizKey { get; set; }
    }
}
