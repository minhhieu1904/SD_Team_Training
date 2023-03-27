
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Commons
{
    public class MS_ShiftDto
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; }
        [Key]
        [StringLength(1)]
        public string Shift { get; set; }
        [Required]
        [StringLength(50)]
        public string ShiftName { get; set; }
    }
}
