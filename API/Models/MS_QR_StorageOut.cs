using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey("manuf", "iono", "QRCodeID")]
[Table("MS_QR_StorageOut")]
public partial class MS_QR_StorageOut
{
    [Key]
    [StringLength(1)]
    public string manuf { get; set; }

    [Required]
    [StringLength(20)]
    public string StorageOutKind { get; set; }

    [Column(TypeName = "date")]
    public DateTime Sdat { get; set; }

    [Column(TypeName = "date")]
    public DateTime? Odat { get; set; }

    [Column(TypeName = "date")]
    public DateTime iodat { get; set; }

    [Key]
    [StringLength(12)]
    public string iono { get; set; }

    [Required]
    [StringLength(12)]
    public string trno { get; set; }

    [Required]
    [StringLength(2)]
    public string storeh { get; set; }

    [Required]
    [StringLength(3)]
    public string parno { get; set; }

    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string Grade { get; set; }

    [Key]
    [StringLength(13)]
    public string QRCodeID { get; set; }

    [Required]
    [StringLength(15)]
    public string manno { get; set; }

    [Required]
    [StringLength(10)]
    public string purno { get; set; }

    [Column(TypeName = "decimal(5, 0)")]
    public decimal serial { get; set; }

    [Required]
    [StringLength(4)]
    public string size { get; set; }

    [Column(TypeName = "decimal(7, 1)")]
    public decimal qty { get; set; }

    [StringLength(6)]
    public string crusr { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? crday { get; set; }

    [Required]
    [StringLength(1)]
    public string biz_flag { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? biz_time { get; set; }

    /// <summary>
    /// 派工單號
    /// </summary>
    [Required]
    [StringLength(10)]
    public string wkshno { get; set; }

    /// <summary>
    /// 列印單號
    /// </summary>
    [Required]
    [StringLength(10)]
    public string prtno { get; set; }

    [Column(TypeName = "decimal(7, 1)")]
    public decimal wkshqty { get; set; }

    [Column(TypeName = "date")]
    public DateTime mdat { get; set; }

    [Required]
    [StringLength(5)]
    public string empno { get; set; }

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
}
