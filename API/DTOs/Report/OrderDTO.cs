

using Microsoft.Data.SqlClient;

namespace API.DTOs.Report
{
   
    public class Report_wksh_SumResult_Param
    {
        public string manuf { get; set; }
        public DateTime? mdat_dateFrom { get; set; }
        public DateTime? mdat_dateTo { get; set; }
        public string close_status { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? eta_dateFrom { get; set; }
        public DateTime? eta_dateTo { get; set; }
        public string size { get; set; }
    }
    public class getBrand
    {
         public string id { get; set; }
        public string brandname { get; set; }

    }

}