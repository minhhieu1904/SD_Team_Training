using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    // MS_Shift
    public class MS_Shift
    {
        [Key]
        [Required]
        [MaxLength(1)]
        public string Manuf { get; set; }

        [Key]
        [Required]
        [MaxLength(1)]
        public string Shift { get; set; }

        [MaxLength(50)]
        public string ShiftName { get; set; }
    }
}