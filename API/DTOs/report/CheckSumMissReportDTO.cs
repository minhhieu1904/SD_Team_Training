
using System;
namespace API.DTOs.Report
{
    public class CheckSumMiss_TB1
    {
        public string Yymmdd { get; set; }
        public decimal? SumPackage { get; set; }
        public decimal? SumPairs { get; set; }
        public decimal? SumSortPack { get; set; }
        public decimal? SumSortPairs { get; set; }
        public decimal? SumLossPairs { get; set; }
        public decimal? SumStoragePack { get; set; }
        public decimal? SumStoragePairs { get; set; }
        public decimal? MissPackage { get; set; }
        public decimal? MissPairs { get; set; }
        public decimal? ProPortion { get; set; }
    }

    public class CheckSumMiss_TB2
    {
        public string Yymmdd { get; set; }
        public string Manuf { get; set; }
        public string Type { get; set; }
        public string ManNo { get; set; }

        public string PurNo { get; set; }
        public string PrtNo { get; set; }
        public string WkshNo { get; set; }
        public string QRCodeID { get; set; }

        public string Rmodel { get; set; }
        public string Tolcls { get; set; }
        public string Bitnbr { get; set; }
        public DateTime? Eta { get; set; }

        public string EmpNo { get; set; }
        public short? Serial { get; set; }
        public string Size { get; set; }
        public decimal? Qty { get; set; }

        public string CrUsr { get; set; }
        public DateTime? CrdaY { get; set; }
        public string SortFlag { get; set; }
        public string StorageFlag { get; set; }
    }

    public class CheckSumMiss_SumDTO
    {
        public DateTime curDate { get; set; }
        public decimal? TotalSumPackage { get; set; }
        public decimal? TotalSumPairs { get; set; }
        public decimal? TotalSumSortPack { get; set; }
        public decimal? TotalSumSortPairs { get; set; }
        public decimal? TotalSumLossPairs { get; set; }
        public decimal? TotalSumStoragePack { get; set; }
        public decimal? TotalSumStoragePairs { get; set; }
        public decimal? TotalMissPackage { get; set; }
        public decimal? TotalMissPairs { get; set; }
        public decimal? TotalProPortion { get; set; }
    }
}