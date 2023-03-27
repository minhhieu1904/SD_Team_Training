
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Dtos.Commons
{
    public class MS_PackageDto
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; }
        [Key]
        [StringLength(4)]
        public string PackageNo { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal PackageQty { get; set; }
    }
}
