using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey("Manuf", "QRCodeID")]
[Table("MS_QR_Label")]
public partial class MS_QR_Label
{
    [Key]
    [StringLength(1)]
    public string Manuf { get; set; }

    [StringLength(1)]
    public string Type { get; set; }

    [Required]
    [StringLength(15)]
    public string ManNo { get; set; }

    [Required]
    [StringLength(10)]
    public string PurNo { get; set; }

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

    [Required]
    [StringLength(1)]
    [Unicode(false)]
    public string Grade { get; set; }

    [Key]
    [StringLength(13)]
    public string QRCodeID { get; set; }

    [StringLength(73)]
    public string QRCodeValue { get; set; }

    [Required]
    [StringLength(5)]
    public string empno { get; set; }

    [StringLength(3)]
    public string Seq { get; set; }

    [StringLength(21)]
    public string CyNo { get; set; }

    public short Serial { get; set; }

    [Required]
    [StringLength(4)]
    public string Size { get; set; }

    [Column(TypeName = "decimal(7, 1)")]
    public decimal Qty { get; set; }

    public short Prt_Cnt { get; set; }

    [Required]
    [StringLength(1)]
    public string Flag { get; set; }

    [Required]
    [StringLength(6)]
    public string CrUsr { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CrDay { get; set; }

    [StringLength(6)]
    public string CanUsr { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CanDay { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime PrtDay { get; set; }

    [StringLength(100)]
    public string RptReason { get; set; }

    [StringLength(6)]
    public string RptUsr { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RptDate { get; set; }
}
