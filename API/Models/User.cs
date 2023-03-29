using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class User
    {
        [Key]
        [Column("account")]
        [StringLength(50)]
        public string Account { get; set; }
        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; }
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
        [Column("update_time", TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
    }
}
