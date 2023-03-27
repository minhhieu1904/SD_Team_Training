namespace API.Dtos.Transaction.ReprintPackingScan
{
    public class ReprintPackingScanModel
    {
        public DateTime SDat { get; set; }
        public string TrNo { get; set; }
        public decimal SortQty { get; set; }
        public string ShiftName { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
        public short Serial { get; set; }
        public decimal pqty { get; set; }
        public string wkshno { get; set; }
        public decimal wkshqty { get; set; }
        public string prtno { get; set; }
        public string rmodel { get; set; }
        public string bitnbr { get; set; }
        public string Shift { get; set; }
        public string CrUsr { get; set; }
    }
}