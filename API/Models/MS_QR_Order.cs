using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// 企劃單明細檔
    /// </summary>
    public partial class MS_QR_Order
    {

        /// <summary>
        /// 廠別
        /// </summary>
        [Key]
        [StringLength(1)]
        public string manuf { get; set; }

        /// <summary>
        /// &#21697;&#29260;&#21517;&#31281;
        /// </summary>
        [Required]
        [StringLength(10)]
        public string brandname { get; set; }

        /// <summary>
        /// &#23458;&#25142;&#32232;&#34399;
        /// </summary>
        [Required]
        [StringLength(40)]
        public string cusid { get; set; }

        /// <summary>
        /// &#23458;&#25142;&#21517;&#31281;
        /// </summary>
        [Required]
        [StringLength(10)]
        public string cusna { get; set; }

        /// <summary>
        /// &#35330;&#21934;&#39006;&#21029;
        /// </summary>
        [Required]
        [StringLength(1)]
        public string kind { get; set; }

        /// <summary>
        /// &#35330;&#36092;&#21934;&#34399;
        /// </summary>
        [Key]
        [StringLength(10)]
        public string purno { get; set; }

        /// <summary>
        /// &#20225;&#21123;&#21934;&#34399;
        /// </summary>
        [Key]
        [StringLength(15)]
        public string manno { get; set; }

        /// <summary>
        /// &#30906;&#35469;&#20132;&#26399;
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? eta { get; set; }

        /// <summary>
        /// &#21697;&#34399;
        /// </summary>
        [Required]
        [StringLength(16)]
        public string bitnbr { get; set; }

        /// <summary>
        /// &#38795;&#24288;&#26009;&#34399;
        /// </summary>
        [Required]
        [StringLength(10)]
        public string ritnbr { get; set; }

        /// <summary>
        /// &#27169;&#20855;&#32232;&#34399;
        /// </summary>
        [Required]
        [StringLength(15)]
        public string rmodel { get; set; }

        /// <summary>
        /// &#24037;&#20855;&#39006;&#21029;
        /// </summary>
        [StringLength(3)]
        public string tolcls { get; set; }

        /// <summary>
        /// Article
        /// </summary>
        [Required]
        [StringLength(6)]
        public string article { get; set; }

        /// <summary>
        /// &#38795;&#22411;
        /// </summary>
        [StringLength(15)]
        public string style { get; set; }

        /// <summary>
        /// &#25509;&#21934;&#23610;&#23544;
        /// </summary>
        [Key]
        [StringLength(4)]
        public string size { get; set; }

        /// <summary>
        /// &#24037;&#20855;&#23610;&#23544;
        /// </summary>
        [Required]
        [StringLength(4)]
        public string tsize { get; set; }

        /// <summary>
        /// &#35330;&#36092;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal qty { get; set; }

        /// <summary>
        /// &#35330;&#36092;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal pqty { get; set; }

        /// <summary>
        /// &#21462;&#28040;&#21015;&#21360;&#38617;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal cqty { get; set; }

        /// <summary>
        /// &#20225;&#21123;&#21934;&#22686;&#21152;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal addqty { get; set; }

        /// <summary>
        /// &#20225;&#21123;&#21934;&#28187;&#23569;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal lessqty { get; set; }

        /// <summary>
        /// &#23436;&#25104;&#21015;&#21360;&#21542;
        /// </summary>
        [Required]
        [StringLength(1)]
        public string endcod { get; set; }
        [Required]
        [StringLength(1)]
        public string cycle_flag { get; set; }

        /// <summary>
        /// &#30064;&#21205;&#26178;&#38291;
        /// </summary>
        [Column(TypeName = "datetime")]
        public DateTime update_time { get; set; }

        /// <summary>
        /// &#30064;&#21205;&#32773;
        /// </summary>
        [StringLength(15)]
        public string updated_by { get; set; }
        [Required]
        [StringLength(30)]
        public string Biz_Key { get; set; }

        /// <summary>
        /// &#27966;&#24037;&#21934;&#34399;
        /// </summary>
        [Key]
        [StringLength(10)]
        public string wkshno { get; set; }

        /// <summary>
        /// &#21015;&#21360;&#21934;&#34399;
        /// </summary>
        [Key]
        [StringLength(10)]
        public string prtno { get; set; }

        /// <summary>
        /// &#27966;&#24037;&#38617;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal wkshqty { get; set; }

        /// <summary>
        /// &#29983;&#29986;&#26085;&#26399;
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime mdat { get; set; }
        [StringLength(1)]
        public string uscod { get; set; }
    }
}
