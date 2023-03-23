using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    /// <summary>
    /// &#22238;&#36681;&#21253;&#35037;&#26126;&#32048;&#27284;
    /// </summary>
    public partial class MS_QR_Cycle
    {
        /// <summary>
        /// 廠別
        /// </summary>
        [Key]
        [StringLength(1)]
        public string manuf { get; set; }
        /// <summary>
        /// &#20225;&#21123;&#21934;&#34399;
        /// </summary>
        [Key]
        [StringLength(15)]
        public string manno { get; set; }
        /// <summary>
        /// &#25209;&#27425;
        /// </summary>
        [Key]
        [StringLength(3)]
        public string seq { get; set; }
        /// <summary>
        /// &#35330;&#36092;&#21934;&#34399;
        /// </summary>
        [Key]
        [StringLength(10)]
        public string purno { get; set; }
        /// <summary>
        /// &#38795;&#22411;
        /// </summary>
        [Required]
        [StringLength(6)]
        public string article { get; set; }
        /// <summary>
        /// &#36852;&#36681;
        /// </summary>
        [Key]
        [StringLength(21)]
        public string cyno { get; set; }
        /// <summary>
        /// &#23610;&#23544;
        /// </summary>
        [Key]
        [StringLength(4)]
        public string size { get; set; }
        /// <summary>
        /// &#38617;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal qty { get; set; }
        /// <summary>
        /// &#24050;&#21015;&#21360;&#38617;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal pqty { get; set; }
        /// <summary>
        /// &#21462;&#28040;&#21015;&#21360;&#38617;&#25976;
        /// </summary>
        [Column(TypeName = "decimal(7, 1)")]
        public decimal cqty { get; set; }
        /// <summary>
        /// &#23436;&#25104;&#21015;&#21360;&#21542;
        /// </summary>
        [Required]
        [StringLength(1)]
        public string endcod { get; set; }
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
        [StringLength(54)]
        public string Biz_Key { get; set; }
    }
}
