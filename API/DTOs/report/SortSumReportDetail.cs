using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.report.SortSumReport
{
    public class SortSumReportDetail
    {
        public string IsScanSort { get; set; }
        public string CrDay { get; set; }
        public string brandname { get; set; }
        public string QRCodeID { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short Serial { get; set; }
        public decimal Qty { get; set; }
        public string cusna { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string ritnbr { get; set; }
        public string bitnbr { get; set; }
        public string article { get; set; }
        public string kind { get; set; }
        public  DateTime? eta { get; set; }
        
    }
    public class SortSumDetailReportParam {
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
    }
}