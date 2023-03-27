
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace API.Models
{
    public class Users
    {
        [Key]
        [Column("account")]
        [StringLength(50)]
        public string Account { get; set; }
        [Required]
        [Column("password")]
        [StringLength(50)]
        public string Password { get; set; }
        [Required]
        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("email")]
        [StringLength(50)]
        public string Email { get; set; }
        [Column("is_active")]
        public bool IsActive { get; set; }
        [Required]
        [Column("update_by")]
        [StringLength(50)]
        public string UpdateBy { get; set; }
        [Column("update_time", TypeName = "datetime")]
        public DateTime UpdateTime { get; set; }
    }
}