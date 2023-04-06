using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class Role
    {
        [Key]
        [Column("role_unique")]
        [StringLength(50)]
        public string RoleUnique { get; set; } = null!;
        [Column("role_name")]
        [StringLength(50)]
        public string RoleName { get; set; } = null!;
        [Column("role_type")]
        [StringLength(50)]
        public string RoleType { get; set; } = null!;
        [Column("role_note")]
        [StringLength(250)]
        public string RoleNote { get; set; } = null!;
        [Column("role_sequence")]
        public double RoleSequence { get; set; }
        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; } = null!;
        [Column("update_time", TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
    }
}
