using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class RoleUser
    {
        [Key]
        [Column("user_account")]
        [StringLength(50)]
        public string UserAccount { get; set; }
        [Key]
        [Column("role_unique")]
        [StringLength(50)]
        public string RoleUnique { get; set; }
        [Required]
        [Column("create_by")]
        [StringLength(50)]
        public string CreateBy { get; set; }
        [Column("create_time", TypeName = "datetime")]
        public DateTime CreateTime { get; set; }
    }
}