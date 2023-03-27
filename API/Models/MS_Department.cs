using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MS_Department
    {
        [Key]
        public string Manuf { get; set; }
        [Key]
        public string ParNo { get; set; }
        public string ParName { get; set; }
    }
}