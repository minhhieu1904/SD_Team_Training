
namespace API.DTOs.Report
{
    public class CheckSumMissReportParam
    {
        public string  rmodel {get;set;}
        public string  tolcls {get;set;}
        public DateTime?  mdat_start {get;set;}
        public DateTime?  mdat_end {get;set;}
    }
}