using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class MS_Shift
    {
        [Key]
        public string Manuf {get; set;}
        [Key]
        public string Shift {get; set;}
        public string ShiftName {get; set;}
        
    }
}