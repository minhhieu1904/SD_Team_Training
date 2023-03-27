namespace API.Helpers.Params.Report
{
    public class QRcodeWIPReportParam
    {
        public string moldno { get; set; }
        public string toolno { get; set; }
        public DateTime? mdat_start { get; set; }
        public DateTime? mdat_end { get; set; }
    }
}