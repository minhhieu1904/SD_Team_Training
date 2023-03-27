namespace API.DTOs.Report
{
    public class CheckSumMissHeaderReportDTO
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

    public class CheckSumMissDetailReportDTO
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

        public string CrdaY_str { get; set; }
        public string SortFlag { get; set; }
        public string StorageFlag { get; set; }
    }

    public class CheckSumMissOtherReportDTO
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

    public class CheckSumMissReportParam
    {
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public DateTime? mdat_start { get; set; }
        public DateTime? mdat_end { get; set; }
    }
}