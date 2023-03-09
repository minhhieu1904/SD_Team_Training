using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class MS_QR_Sort
    {
        [Key]
        [StringLength(1)]
        public string Manuf { get; set; }
        [Column(TypeName = "date")]
        public DateTime SDat { get; set; }
        [Key]
        [StringLength(12)]
        public string TrNo { get; set; }
        [Required]
        [StringLength(1)]
        public string Shift { get; set; }
        [Required]
        [StringLength(1)]
        public string Grade { get; set; }
        [Key]
        [StringLength(13)]
        public string QRCodeID { get; set; }
        [Required]
        [StringLength(15)]
        public string ManNo { get; set; }
        [Required]
        [StringLength(15)]
        public string PurNo { get; set; }
        public short Serial { get; set; }
        [Required]
        [StringLength(4)]
        public string Size { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal Qty { get; set; }
        [Required]
        [StringLength(6)]
        public string CrUsr { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CrDay { get; set; }
        [Required]
        [StringLength(1)]
        public string EndCod { get; set; }
    }
}
