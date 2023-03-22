﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("MS_Department")]
    public partial class MsDepartment
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [Key]
        [StringLength(3)]
        public string ParNo { get; set; } = null!;
        [StringLength(50)]
        public string ParName { get; set; } = null!;
    }
}
