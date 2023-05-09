using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper.Params
{
    public class SearchForCyclePackingDataParam
    {
        public string manuf { get; set; }
        public string manno { get; set; }
        public string seq { get; set; }
        public string purno { get; set; }
        public string article { get; set; }
        public string cyno { get; set; }
        public string size { get; set; }
        public string endcod { get; set; }
        public string Biz_Key { get; set; }
    }
}