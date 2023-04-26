using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.SearchForCyclePackingData
{
    public class SearchForCyclePackingDataParam 
    {
        public string rmodel { get; set; }
        public string cusid { get; set; }
        public string cusna { get; set; }
        public string bitnbr { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
        public string prtno { get; set; }
        public string seq { get; set; }
        public string endcod { get; set; }

    }
}