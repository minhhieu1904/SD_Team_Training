using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey("Manuf", "StoreH")]
[Table("MS_Location")]
public partial class MS_Location
{
    [Key]
    [StringLength(1)]
    public string Manuf { get; set; }

    [Key]
    [StringLength(2)]
    public string StoreH { get; set; }

    [Required]
    [StringLength(50)]
    public string LocationName { get; set; }
}
