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
        public string Manuf { get; set; }
        [Key]
        public string StoreH { get; set; }
        public string LocationName { get; set; }
    }
}