namespace API.Dtos.Transaction.ReprintSticker
{
    public class ReprintStickerModel
    {
        public string Manuf {get;set;}
        public string QRCodeID {get;set;}
        public DateTime? mdat { get; set; }
        public string Grade { get; set; }
        public string prtno { get; set; }
        public string wkshno { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short Serial { get; set; }
        public decimal Qty { get; set; }
        public short Prt_Cnt { get; set; }
        public string QRCodeValue { get; set; }
        public string brandname { get; set; }
        public string cusid { get; set; }
        public string cusna { get; set; }
        public string bitnbr { get; set; }
        public string ritnbr { get; set; }
        public string rmodel { get; set; }
        public string article { get; set; }
        public string empno { get; set; }
        public string CrUsr { get; set; }
        public string kind { get; set; }
        public DateTime? PrtDay { get; set; }
        public string remark { get; set; }
    }
}