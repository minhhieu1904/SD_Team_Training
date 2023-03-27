using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Report
{
    public class MS_QR_StorageDto
    {
        public string Manuf { get; set; }
        public DateTime Sdat { get; set; }
        public DateTime Iodat { get; set; }
        public string Trno { get; set; }
        public string Storeh { get; set; }
        public string Parno { get; set; }
        public string Grade { get; set; }
        public string QrcodeId { get; set; }
        public string Manno { get; set; }
        public string Purno { get; set; }
        public decimal Serial { get; set; }
        public string Size { get; set; }
        public decimal Qty { get; set; }
        public string Crusr { get; set; }
        public DateTime Crday { get; set; }
        public string BizFlag { get; set; }
        public DateTime? BizTime { get; set; }
        public string Wkshno { get; set; }
        public string Prtno { get; set; }
        public decimal Wkshqty { get; set; }
        public DateTime Mdat { get; set; }
        public string Uscod { get; set; }
        public string Empno { get; set; }
        public string Bitnbr { get; set; }
        public string Ritnbr { get; set; }
        public string IsPicking { get; set; }
        public string IsStorageOut { get; set; }
    }
    public class ReportStorageSum
    {
        public DateTime? date_kind { get; set; }
        public DateTime? date_start { get; set; }
        public DateTime? date_end { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? etd_start { get; set; }
        public DateTime? etd_end { get; set; }
        public string size { get; set; }
    }
    public class ReportStorageSumDetail
    {
        public string IsStorageSort { get; set; }
        public string QrcodeId { get; set; }
        public DateTime? CrDay { get; set; }
        public string Brandname { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short Serial { get; set; }
        public decimal Qty { get; set; }
        public string Cusna { get; set; }
        public string Rmodel { get; set; }
        public string Tolcls { get; set; }
        public string Ritnbr { get; set; }
        public string Bitnbr { get; set; }
        public string Article { get; set; }
        public string Kind { get; set; }
        public DateTime? Eta { get; set; }
    }
}