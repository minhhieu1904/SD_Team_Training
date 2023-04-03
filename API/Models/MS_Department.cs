﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[PrimaryKey("Manuf", "ParNo")]
[Table("MS_Department")]
public partial class MS_Department
{
    [Key]
    [StringLength(1)]
    public string Manuf { get; set; }

    [Key]
    [StringLength(3)]
    public string ParNo { get; set; }

    [Required]
    [StringLength(50)]
    public string ParName { get; set; }
}
