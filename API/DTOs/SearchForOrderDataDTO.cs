
namespace API.DTOs.Trainsaction
{

    public class SearchForOrderDataDTO
    {
        public string brandname { get; set; }
        public string rmodel { get; set; }
        public string bitnbr { get; set; }
        public string size { get; set; }
        public string cusid { get; set; }

        public string manno { get; set; }
        public string purno { get; set; }
        public string endcod { get; set; }
        public string wkshno { get; set; }
        public string cusna { get; set; }

        public string kind { get; set; }
        public DateTime? eta { get; set; }
        public string article { get; set; }
        public decimal qty { get; set; }
        public decimal wkshqty { get; set; }

        public decimal addqty { get; set; }
        public decimal lessqty { get; set; }
        public decimal apqty { get; set; }
        public decimal pqty { get; set; }
        public decimal cqty { get; set; }
        public string ritnbr { get; set; }
        public string style { get; set; }
        public string tsize { get; set; }
        public string remark { get; set; }
        public DateTime update_time { get; set; }
        public string updated_by { get; set; }

        public string prtno { get; set; }
        public decimal avaiablePqty { get => (wkshqty - pqty); }
        public bool isChecked { get => false; set => value = isChecked; }
        public bool isDisabled { get; set; }
    }

    public class SearchForOrderDataPrint
    {
        public string Mannuf { get; set; }
        public string Type { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string wkshNo { get; set; }

        public string PrtNo { get; set; }
        public string Grade { get; set; }
        public string QRCodeId { get; set; }
        public string QRCodeValue { get; set; }
        public string EmpNo { get; set; }
        public string Seq { get; set; }
        public string CyNo { get; set; }
        public string Serial { get; set; }
        public string Size { get; set; }
        public string Qty { get; set; }
        public string Prt_Cnt { get; set; }
        public string Flag { get; set; }
        public string CrUsr { get; set; }
        public string CrDay { get; set; }
        public string CanUsr { get; set; }
        public string CanDay { get; set; }
        public string PrtDay { get; set; }
        public string RptReason { get; set; }
        public string PrtUsr { get; set; }
        public DateTime? RptDate { get; set; }
    }

    public class OrderDataPrint
    {
        public List<OrderDataItem> Items { get; set; }
        public decimal PackageQty { get; set; }
        public decimal PrintQty { get; set; }
        public bool Balance { get; set; }
        public string Empno { get; set; }
        public bool IsB_grade { get; set; }
        public string Grade { get => IsB_grade ? "B" : "A"; }
        public string UserName { get; set; }
        public bool Remark { get; set; }
    }
    public class OrderDataItem
    {
        public string purno { get; set; }
        public string manno { get; set; }
        public string size { get; set; }
        public string wkshno { get; set; }
        public decimal wkshqty { get; set; }
        public string prtno { get; set; }
        public decimal maxPqty { get; set; }
    }

    public class CompareQtyResult
    {
        public decimal qty { get; set; }
        public decimal pqty { get; set; }
        public decimal printQty { get; set; }
    }
}