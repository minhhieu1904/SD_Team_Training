using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_Package")]
    public partial class MsPackage
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [Key]
        [StringLength(4)]
        public string PackageNo { get; set; } = null!;
        [Column(TypeName = "decimal(7, 1)")]
        public decimal PackageQty { get; set; }
    }
}
