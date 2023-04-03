using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey("manuf", "purno", "manno", "size", "wkshno", "prtno")]
[Table("MS_QR_Order_Log_OnlyForST")]
public partial class MS_QR_Order_Log_OnlyForST
{
    /// <summary>
    /// 廠別
    /// </summary>
    [Key]
    [StringLength(1)]
    public string manuf { get; set; }

    /// <summary>
    /// 品牌名稱
    /// </summary>
    [Required]
    [StringLength(10)]
    public string brandname { get; set; }

    /// <summary>
    /// 客戶編號
    /// </summary>
    [Required]
    [StringLength(40)]
    public string cusid { get; set; }

    /// <summary>
    /// 客戶名稱
    /// </summary>
    [Required]
    [StringLength(10)]
    public string cusna { get; set; }

    /// <summary>
    /// 訂單類別
    /// </summary>
    [Required]
    [StringLength(1)]
    public string kind { get; set; }

    /// <summary>
    /// 訂購單號
    /// </summary>
    [Key]
    [StringLength(10)]
    public string purno { get; set; }

    /// <summary>
    /// 企劃單號
    /// </summary>
    [Key]
    [StringLength(15)]
    public string manno { get; set; }

    /// <summary>
    /// 確認交期
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime? eta { get; set; }

    /// <summary>
    /// 品號
    /// </summary>
    [Required]
    [StringLength(16)]
    public string bitnbr { get; set; }

    /// <summary>
    /// 鞋廠料號
    /// </summary>
    [Required]
    [StringLength(10)]
    public string ritnbr { get; set; }

    /// <summary>
    /// 模具編號
    /// </summary>
    [Required]
    [StringLength(15)]
    public string rmodel { get; set; }

    /// <summary>
    /// 工具類別
    /// </summary>
    [StringLength(3)]
    public string tolcls { get; set; }

    /// <summary>
    /// Article
    /// </summary>
    [Required]
    [StringLength(6)]
    public string article { get; set; }

    /// <summary>
    /// 鞋型
    /// </summary>
    [StringLength(15)]
    public string style { get; set; }

    /// <summary>
    /// 接單尺寸
    /// </summary>
    [Key]
    [StringLength(4)]
    public string size { get; set; }

    /// <summary>
    /// 工具尺寸
    /// </summary>
    [Required]
    [StringLength(4)]
    public string tsize { get; set; }

    /// <summary>
    /// 訂購數
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal qty { get; set; }

    /// <summary>
    /// 訂購數
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal pqty { get; set; }

    /// <summary>
    /// 取消列印雙數
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal cqty { get; set; }

    /// <summary>
    /// 企劃單增加
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal addqty { get; set; }

    /// <summary>
    /// 企劃單減少
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal lessqty { get; set; }

    /// <summary>
    /// 完成列印否
    /// </summary>
    [Required]
    [StringLength(1)]
    public string endcod { get; set; }

    [Required]
    [StringLength(1)]
    public string cycle_flag { get; set; }

    /// <summary>
    /// 異動時間
    /// </summary>
    [Column(TypeName = "datetime")]
    public DateTime update_time { get; set; }

    /// <summary>
    /// 異動者
    /// </summary>
    [StringLength(15)]
    public string updated_by { get; set; }

    /// <summary>
    /// 派工單號
    /// </summary>
    [Key]
    [StringLength(10)]
    public string wkshno { get; set; }

    /// <summary>
    /// 列印單號
    /// </summary>
    [Key]
    [StringLength(10)]
    public string prtno { get; set; }

    /// <summary>
    /// 派工雙數
    /// </summary>
    [Column(TypeName = "decimal(7, 1)")]
    public decimal wkshqty { get; set; }

    /// <summary>
    /// 生產日期
    /// </summary>
    [Column(TypeName = "date")]
    public DateTime mdat { get; set; }

    [StringLength(1)]
    public string uscod { get; set; }
}
