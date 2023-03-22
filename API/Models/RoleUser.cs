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
        [StringLength(50)]
        public string user_account { get; set; }
        [Key]
        [StringLength(50)]
        public string role_unique { get; set; }
        [Required]
        [StringLength(50)]
        public string create_by { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime create_time { get; set; }
    }
}