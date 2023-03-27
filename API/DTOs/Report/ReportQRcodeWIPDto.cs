using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SDCores;

namespace API.Dtos.Report
{
    public class Report_Check_Sum_Miss
    {
        public List<ReportCheckSumMissHeader> Headers { get; set; }
        public PaginationUtility<ReportCheckSumMissDetail> Details { get; set; }
    }

    public class ReportCheckSumMissHeader
    {
        [StringLength(6)]
        public string yymmdd { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? Sumpackage { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? Sumpairs { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? SumSortPack { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? SumSortPairs { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? SumlossPairs { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? SumStoragePack { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? SumStoragePairs { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? MissPackage { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? MissPairs { get; set; }
        [Column(TypeName = "decimal(6, 4)")]
        public decimal? Proportion { get; set; }
    }

    public class ReportCheckSumMissDetail
    {
        [StringLength(6)]
        public string yymmdd { get; set; }
        public string Manuf { get; set; }
        public string Type { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string wkshno { get; set; }
        public string prtno { get; set; }
        public string QRCodeID { get; set; }
        public string model { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public DateTime? eta { get; set; }
        public string empno { get; set; }
        public short? serial { get; set; }
        public string size { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal? qty { get; set; }
        public string CrUsr { get; set; }
        public DateTime? crdaY { get; set; }
        public string crdaY_Format { get => crdaY.HasValue ? crdaY.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; }
        public string Sortflag { get; set; }
        public string Storageflag { get; set; }
    }
}