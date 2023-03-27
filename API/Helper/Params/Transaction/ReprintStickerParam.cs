namespace API.Helpers.Params.Transaction
{
    public class ReprintStickerParam
    {
        public string fromDate {get;set;}
        public string toDate {get;set;}
        public string prtno {get;set;}
        public int serial {get;set;}
        public string ManNo { get; set; }
        public string Size { get; set; }
        public string PurNo { get; set; }
    }
}