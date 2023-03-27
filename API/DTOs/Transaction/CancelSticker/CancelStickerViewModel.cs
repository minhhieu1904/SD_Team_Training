namespace API.Dtos.Transaction.CancelSticker
{
    public class CancelStickerViewModel
    {
        public string IsSortScan { get; set; }
        public string Grade { get; set; }
        public string wkshno { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short Serial { get; set; }
        public decimal Qty { get; set; }
        public short Prt_Cnt { get; set; }
        public string QRCodeID { get; set; }
        public string QRCodeValue { get; set; }
        public string empno { get; set; }
        public string CrUsr { get; set; }
        public DateTime PrtDay { get; set; }
        public string Flag { get; set; }
    }
}