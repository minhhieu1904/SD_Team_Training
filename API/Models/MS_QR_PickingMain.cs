using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey("manuf", "pickingTrNo")]
[Table("MS_QR_PickingMain")]
public partial class MS_QR_PickingMain
{
    [Key]
    [StringLength(1)]
    public string manuf { get; set; }

    [Key]
    [StringLength(12)]
    public string pickingTrNo { get; set; }

    [Required]
    [StringLength(2)]
    public string storeh { get; set; }

    [Required]
    [StringLength(3)]
    public string parno { get; set; }

    [StringLength(6)]
    public string crusr { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? crday { get; set; }

    [Column(TypeName = "date")]
    public DateTime? ReleaseDate { get; set; }

    [Required]
    [StringLength(10)]
    public string status { get; set; }
}
