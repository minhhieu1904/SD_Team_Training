using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    /// <summary>
    /// 企劃單明細檔
    /// </summary>
    [Table("MS_QR_Order")]
    public partial class MsQrOrder
    {
        /// <summary>
        /// 廠別
        /// </summary>
        [Key]
        [Column("manuf")]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        /// <summary>
        /// 品牌名稱
        /// </summary>
        [Column("brandname")]
        [StringLength(10)]
        public string Brandname { get; set; } = null!;
        /// <summary>
        /// 客戶編號
        /// </summary>
        [Column("cusid")]
        [StringLength(40)]
        public string Cusid { get; set; } = null!;
        /// <summary>
        /// 客戶名稱
        /// </summary>
        [Column("cusna")]
        [StringLength(10)]
        public string Cusna { get; set; } = null!;
        /// <summary>
        /// 訂單類別
        /// </summary>
        [Column("kind")]
        [StringLength(1)]
        public string Kind { get; set; } = null!;
        /// <summary>
        /// 訂購單號
        /// </summary>
        [Key]
        [Column("purno")]
        [StringLength(10)]
        public string Purno { get; set; } = null!;
        /// <summary>
        /// 企劃單號
        /// </summary>
        [Key]
        [Column("manno")]
        [StringLength(15)]
        public string Manno { get; set; } = null!;
        /// <summary>
        /// 確認交期
        /// </summary>
        [Column("eta", TypeName = "date")]
        public DateTime? Eta { get; set; }
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
        /// <summary>
        /// 模具編號
        /// </summary>
        [Column("rmodel")]
        [StringLength(15)]
        public string Rmodel { get; set; } = null!;
        /// <summary>
        /// 工具類別
        /// </summary>
        [Column("tolcls")]
        [StringLength(3)]
        public string? Tolcls { get; set; }
        /// <summary>
        /// Article
        /// </summary>
        [Column("article")]
        [StringLength(6)]
        public string Article { get; set; } = null!;
        /// <summary>
        /// 鞋型
        /// </summary>
        [Column("style")]
        [StringLength(15)]
        public string? Style { get; set; }
        /// <summary>
        /// 接單尺寸
        /// </summary>
        [Key]
        [Column("size")]
        [StringLength(4)]
        public string Size { get; set; } = null!;
        /// <summary>
        /// 工具尺寸
        /// </summary>
        [Column("tsize")]
        [StringLength(4)]
        public string Tsize { get; set; } = null!;
        /// <summary>
        /// 訂購數
        /// </summary>
        [Column("qty", TypeName = "decimal(7, 1)")]
        public decimal Qty { get; set; }
        /// <summary>
        /// 訂購數
        /// </summary>
        [Column("pqty", TypeName = "decimal(7, 1)")]
        public decimal Pqty { get; set; }
        /// <summary>
        /// 取消列印雙數
        /// </summary>
        [Column("cqty", TypeName = "decimal(7, 1)")]
        public decimal Cqty { get; set; }
        /// <summary>
        /// 企劃單增加
        /// </summary>
        [Column("addqty", TypeName = "decimal(7, 1)")]
        public decimal Addqty { get; set; }
        /// <summary>
        /// 企劃單減少
        /// </summary>
        [Column("lessqty", TypeName = "decimal(7, 1)")]
        public decimal Lessqty { get; set; }
        /// <summary>
        /// 完成列印否
        /// </summary>
        [Column("endcod")]
        [StringLength(1)]
        public string Endcod { get; set; } = null!;
        [Column("cycle_flag")]
        [StringLength(1)]
        public string CycleFlag { get; set; } = null!;
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
        public string? UpdatedBy { get; set; }
        [Column("Biz_Key")]
        [StringLength(30)]
        public string BizKey { get; set; } = null!;
        /// <summary>
        /// 派工單號
        /// </summary>
        [Key]
        [Column("wkshno")]
        [StringLength(10)]
        public string Wkshno { get; set; } = null!;
        /// <summary>
        /// 列印單號
        /// </summary>
        [Key]
        [Column("prtno")]
        [StringLength(10)]
        public string Prtno { get; set; } = null!;
        /// <summary>
        /// 派工雙數
        /// </summary>
        [Column("wkshqty", TypeName = "decimal(7, 1)")]
        public decimal Wkshqty { get; set; }
        /// <summary>
        /// 生產日期
        /// </summary>
        [Column("mdat", TypeName = "date")]
        public DateTime Mdat { get; set; }
        [Column("uscod")]
        [StringLength(1)]
        public string? Uscod { get; set; }

        [Column("remark")]
        [StringLength(20)]
        public string? Remark { get; set; }
    }
}
