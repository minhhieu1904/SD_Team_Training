namespace API.Helpers
{
    public class Report_Storage_Sum_Param
    {
        public string crday { get; set; }
        public string sdat { get; set; }
        public string mdat { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string size { get; set; }
    }

    public class SearchSortSumReportParams
    {
        public string crday { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string tolcls { get; set; }
        public string rmodel { get; set; }
        public string purno { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public string etaFrom { get; set; }
        public string etaTo { get; set; }
        public string size { get; set; }
    }
}