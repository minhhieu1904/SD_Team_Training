using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

[Table("MS_Warehouse")]
public partial class MS_Warehouse
{
    [Key]
    [StringLength(1)]
    public string WarehouseID { get; set; }

    [Required]
    [StringLength(10)]
    public string Warehouse { get; set; }

    [StringLength(50)]
    public string WarehouseName { get; set; }
}
