using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.SearchForOrderData
{
    public class SearchForOrderDataParam
    {

        public string brandname { get; set; }
        public string manno { get; set; }
        public string size { get; set; }
        public string rmodel { get; set; }
        public string bitnbr { get; set; }
        public string cusid { get; set; }
        public string cusna { get; set; }
        public string prtno { get; set; }
        public Boolean remark { get; set; }
        public string endcod { get; set; }

    }
}