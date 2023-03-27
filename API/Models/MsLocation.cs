using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("MS_Location")]
    public partial class MsLocation
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; } = null!;
        [Key]
        [StringLength(2)]
        public string StoreH { get; set; } = null!;
        [StringLength(50)]
        public string LocationName { get; set; } = null!;
    }
}
