using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MS_Package
    {
        [Key]
        public string Manuf { get; set; }
        [Key]
        public string PackageNo { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal PackageQty { get; set; }
    }
}