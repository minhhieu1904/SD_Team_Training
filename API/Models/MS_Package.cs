﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey("Manuf", "PackageNo")]
[Table("MS_Package")]
public partial class MS_Package
{
    [Key]
    [StringLength(1)]
    public string Manuf { get; set; }

    [Key]
    [StringLength(4)]
    public string PackageNo { get; set; }

    [Column(TypeName = "decimal(7, 1)")]
    public decimal PackageQty { get; set; }
}