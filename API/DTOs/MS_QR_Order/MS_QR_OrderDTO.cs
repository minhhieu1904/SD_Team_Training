using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MS_QR_Order
{
    public class MS_QR_OrderDTO
    {
        public string manuf { get; set; }
        public string brandname { get; set; }
        public string cusid { get; set; }
        public string cusna { get; set; }
        public string kind { get; set; }
        public string purno { get; set; }
        public string manno { get; set; }
        public DateTime? eta { get; set; }
        public string bitnbr { get; set; }
        public string ritnbr { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string article { get; set; }
        public string style { get; set; }
        public string size { get; set; }
        public string tsize { get; set; }
        public decimal qty { get; set; }
        public decimal pqty { get; set; }
        public decimal cqty { get; set; }
        public decimal addqty { get; set; }
        public decimal lessqty { get; set; }
        public string endcod { get; set; }
        public string cycle_flag { get; set; }
        public DateTime update_time { get; set; }
        public string updated_by { get; set; }
        public string Biz_Key { get; set; }
        public string wkshno { get; set; }
        public string prtno { get; set; }
        public decimal wkshqty { get; set; }
        public DateTime mdat { get; set; }
        public string uscod { get; set; }
    }
}