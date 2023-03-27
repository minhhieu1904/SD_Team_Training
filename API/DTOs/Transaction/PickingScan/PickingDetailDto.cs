namespace API.Dtos.Transaction.PickingScan
{
    public class PickingDetailDto
    {
        public string manuf { get; set; }
        public DateTime Sdat { get; set; }
        public string iono { get; set; }
        public DateTime? iodat { get; set; }
        public string declaration_no { get; set; }
        public string storageTrNo { get; set; }
        public string Grade { get; set; }
        public string QRCodeID { get; set; }
        public string QRCodeValue { get; set; } // Mã Label QR code
        public string manno { get; set; }
        public string purno { get; set; }
        public decimal serial { get; set; }
        public string size { get; set; }
        public decimal qty { get; set; }    
        public string crusr { get; set; }
        public DateTime? crday { get; set; }
        public string wkshno { get; set; }
        public string prtno { get; set; }
        public decimal wkshqty { get; set; }
        public DateTime mdat { get; set; }
        public string empno { get; set; }
        public string bitnbr { get; set; }
        public string ritnbr { get; set; }
        public bool isDelete { get; set; }
        public bool isUpdate { get; set; }
        public bool checkDuplicateAddNew { get; set; }

        // Mã QR code
    }
}