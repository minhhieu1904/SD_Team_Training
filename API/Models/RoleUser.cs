using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    [Table("RoleUser")]
    public partial class RoleUser
    {
        [Key]
        [Column("user_account")]
        [StringLength(50)]
        public string UserAccount { get; set; } = null!;
        [Key]
        [Column("role_unique")]
        [StringLength(50)]
        public string RoleUnique { get; set; } = null!;
        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; } = null!;
        [Column("create_time", TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}
