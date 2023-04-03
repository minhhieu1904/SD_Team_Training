using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Report
{
    public class OrderDTO
    {
        public string manuf { get; set; }
        public DateTime mdat { get; set; }
        public decimal qty { get; set; }
        public string brandname { get; set; }
        public string cusid { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string ritnbr { get; set; }
        public string bitnbr { get; set; }
        public string style { get; set; }
        public string article { get; set; }
        public string kind { get; set; }
        public DateTime? eta { get; set; }
        public string size { get; set; }
        public string endcod { get; set; }
        public decimal oqty { get; set; }
        public decimal wkshqty { get; set; }
        public decimal pqty { get; set; }
        public decimal sqty { get; set; }
        public decimal storageqty { get; set; }
        public string tsize { get; set; }
        public decimal cqty { get; set; }

    }
    public class OderParam
    {
        public string manuf { get; set; }
        public string mdat_dateFrom { get; set; }
        public string mdat_dateTo { get; set; }
        public decimal qty { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public string eta_dateFrom{ get; set; }
        public string eta_dateTo{ get; set; }  
        public string size { get; set; }


    }


}