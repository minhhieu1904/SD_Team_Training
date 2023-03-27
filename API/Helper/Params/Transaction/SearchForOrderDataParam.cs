namespace API.Helpers.Params.Transaction
{
    public class SearchForOrderDataParam
    {
        // Brand
        public string brandname { get; set; }
        // Mold Number
        public string rmodel { get; set; }
        // Material Code
        public string bitnbr { get; set; }
        // Size
        public string size { get; set; }
        // Code of Customer
        public string cusid { get; set; }
        // Planning No
        public string manno { get; set; }
        // Status
        public string endcod { get; set; }
        // Name of Customer
        public string cusna { get; set; }
        public string prtno { get; set; }
        public string tolcls { get; set; }
        public bool is_remark { get; set; }
    }


}