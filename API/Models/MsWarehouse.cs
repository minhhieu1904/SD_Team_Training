using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_Warehouse")]
    public partial class MsWarehouse
    {
        [Key]
        [Column("WarehouseID")]
        [StringLength(1)]
        public string WarehouseId { get; set; } = null!;
        [StringLength(10)]
        public string Warehouse { get; set; } = null!;
        [StringLength(50)]
        public string? WarehouseName { get; set; }
    }
}
