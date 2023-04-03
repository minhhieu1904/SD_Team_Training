using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

/// <summary>
/// 回轉包裝明細檔
/// </summary>
[PrimaryKey("purno", "manno", "seq", "cyno", "size", "manuf")]
[Table("MS_QR_Cycle")]
public partial class MS_QR_Cycle
{
    /// <summary>
    /// 廠別
    /// </summary>
    [Key]
    [StringLength(1)]
    [Unicode(false)]
    public string manuf { get; set; }

    /// <summary>
    /// 企劃單號
    /// </summary>
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string manno { get; set; }

    /// <summary>
    /// 批次
    /// </summary>
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string seq { get; set; }

    /// <summary>
    /// 訂購單號
    /// </summary>
    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string purno { get; set; }

    /// <summary>
    /// 鞋型
    /// </summary>
    [Required]
    [StringLength(6)]
    [Unicode(false)]
    public string article { get; set; }

    /// <summary>
    /// 迴轉
    /// </summary>
    [Key]
    [StringLength(21)]
    [Unicode(false)]
    public string cyno { get; set; }

    /// <summary>
    /// 尺寸
    /// </summary>
    [Key]
    [StringLength(4)]
    [Unicode(false)]
    public string size { get; set; }

    /// <summary>
    /// 雙數
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal qty { get; set; }

    /// <summary>
    /// 已列印雙數
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal pqty { get; set; }

    /// <summary>
    /// 取消列印雙數
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal cqty { get; set; }

    /// <summary>
    /// 完成列印否
    /// </summary>
    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string endcod { get; set; }

    /// <summary>
    /// 異動時間
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime update_time { get; set; }

    /// <summary>
    /// 異動者
    /// </summary>
    [StringLength(15)]
    [Unicode(false)]
    public string updated_by { get; set; }

    [StringLength(54)]
    [Unicode(false)]
    public string Biz_Key { get; set; }
}
