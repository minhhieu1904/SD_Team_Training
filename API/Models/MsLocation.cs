using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("MS_Location")]
    public partial class MsLocation
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; }
        [Key]
        [StringLength(2)]
        public string StoreH { get; set; }
        [StringLength(50)]
        public string LocationName { get; set; }
    }
}
