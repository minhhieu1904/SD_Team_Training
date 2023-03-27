
using System.ComponentModel.DataAnnotations;

namespace API.Dtos.Commons
{
    public class MS_LocationDto
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; }
        [Key]
        [StringLength(2)]
        public string StoreH { get; set; }
        [Required]
        [StringLength(50)]
        public string LocationName { get; set; }
    }
}
