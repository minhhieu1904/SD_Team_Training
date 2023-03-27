using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MS_QR_OrderDto
    {
        public string Manuf { get; set; }
        public string Brandname { get; set; }
        public string Cusid { get; set; }
        public string Cusna { get; set; }
        public string Kind { get; set; }
        public string Purno { get; set; }
        public string Manno { get; set; }
        public DateTime? Eta { get; set; }
        public string Bitnbr { get; set; }
        public string Ritnbr { get; set; }
        public string Rmodel { get; set; }
        public string Tolcls { get; set; }
        public string Article { get; set; }
        public string Style { get; set; }
        public string Size { get; set; }
        public string Tsize { get; set; }
        public decimal Pqty { get; set; }
        public decimal Cqty { get; set; }
        public decimal Addqty { get; set; }
        public decimal Lessqty { get; set; }
        public string Endcod { get; set; }
        public string CycleFlag { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdatedBy { get; set; }
        public string BizKey { get; set; }
        public string Wkshno { get; set; }
        public string Prtno { get; set; }
        public decimal Wkshqty { get; set; }
        public DateTime Mdat { get; set; }
        public string Uscod { get; set; }
    }

    public class ReportWkshSum
    {
        public DateTime? mdat_start { get; set; }
        public DateTime? mdat_end { get; set; }
        public string close_status { get; set; }
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