namespace API.Helpers.Params.Transaction
{
    public class PickingSCanBaseParam
    {
        public string Manuf { get; set; }
        public string Iono { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
    }

    public class PickingScanParam : PickingSCanBaseParam
    {
        public string DeclarationNo { get; set; }
        public string Invno { get; set; }
        public string StartReleaseDate { get; set; }
        public string EndReleaseDate { get; set; }

        public string CurrentUser { get; set; }
    }

    public class PickingScanItemParam
    {
        public string Iono { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set;}
        public string Size { get; set; }
    }
}