using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.report
{
    public class SortSumReport
    {
        public string date_kind {get;set;}
        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string purno { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? eta_start { get; set; }
        public DateTime? eta_end { get; set; }
        public string size { get; set; }

    }
}