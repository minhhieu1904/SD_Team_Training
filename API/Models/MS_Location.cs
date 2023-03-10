using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MS_Location
    {
        [Key]
        [Required]
        [MaxLength(1)]
        public string Manuf { get; set; }

        [Key]
        [Required]
        [MaxLength(1)]
        public string StoreH { get; set; }

        [MaxLength(50)]
        public string LocationName { get; set; }
    }
}