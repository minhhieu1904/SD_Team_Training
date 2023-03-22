using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_Shift")]
    public partial class MsShift
    {
        [Key]
        [Required]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [Key]
        [StringLength(1)]
        public string Shift { get; set; } = null!;
        [StringLength(50)]
        public string ShiftName { get; set; } = null!;
    }
}
