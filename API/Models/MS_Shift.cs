
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public partial class MS_Shift
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
