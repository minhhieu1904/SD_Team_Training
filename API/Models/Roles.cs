using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Roles
    {
        [Key]
        [Column("role_unique")]
        [StringLength(50)]
        public string RoleUnique { get; set; }
        [Required]
        [Column("role_name")]
        [StringLength(50)]
        public string RoleName { get; set; }
        [Required]
        [Column("role_type")]
        [StringLength(50)]
        public string RoleType { get; set; }
        [Required]
        [Column("role_note")]
        [StringLength(250)]
        public string RoleNote { get; set; }
        [Column("role_sequence")]
        public double RoleSequence { get; set; }
        [Required]
        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
        [Column("update_time", TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
    }
}