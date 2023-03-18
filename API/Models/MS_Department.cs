
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class MS_Department
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; }
        [Key]
        [StringLength(3)]
        public string ParNo { get; set; }
        [Required]
        [StringLength(50)]
        public string ParName { get; set; }
    }
}