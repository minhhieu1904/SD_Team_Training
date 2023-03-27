using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public partial class MS_QR_PickingMain
    {

        [Key]
        [StringLength(1)]
        public string manuf { get; set; }

        [Required]
        [StringLength(12)]
        public string declaration_no { get; set; }

        [Required]
        [StringLength(30)]
        public string invno { get; set; }

        [Key]
        [StringLength(18)]
        public string iono { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpectStorageOutDate { get; set; }

        [Key]
        [StringLength(15)]
        public string manno { get; set; }

        [Key]
        [StringLength(10)]
        public string purno { get; set; }

        [Key]
        [StringLength(4)]
        public string size { get; set; }

        [Column(TypeName = "decimal(7, 1)")]
        public decimal? ExpectQTY { get; set; }

        [Column(TypeName = "decimal(7, 1)")]
        public decimal? ActualQTY { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ReleaseDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? StorageOutDate { get; set; }

        [Required]

        [StringLength(10)]
        public string status { get; set; }


        [StringLength(6)]
        public string updated_by { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? update_time { get; set; }
    }
}