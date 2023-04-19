using System.ComponentModel.DataAnnotations;
using SD3_API.Helpers.Utilities;

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
        public decimal? Sumpackage { get; set; }
        public decimal? Sumpairs { get; set; }
        public decimal? SumSortPack { get; set; }
        public decimal? SumSortPairs { get; set; }
        public decimal? SumlossPairs { get; set; }
        public decimal? SumStoragePack { get; set; }
        public decimal? SumStoragePairs { get; set; }
        public decimal? MissPackage { get; set; }
        public decimal? MissPairs { get; set; }
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
        public decimal? qty { get; set; }
        public string CrUsr { get; set; }
        public DateTime? crdaY { get; set; }
        public string crdaY_Format { get => crdaY.HasValue ? crdaY.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; }
        public string Sortflag { get; set; }
        public string Storageflag { get; set; }
    }
}