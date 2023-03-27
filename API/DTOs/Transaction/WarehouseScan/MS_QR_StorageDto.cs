namespace API.DTOs.Transaction.WarehouseScan
{
    public class MS_QR_StorageDto
    {
        public string Manuf { get; set; }
        public DateTime SDat { get; set; }
        public string TrNo { get; set; }
        public string StoreH { get; set; }
        public string ParNo { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public decimal Serial { get; set; }
        public string Size { get; set; }
        public decimal Qty { get; set; }
        public string RModel { get; set; }
        public string BitNbr { get; set; }
        public string Style { get; set; }
        public string Article { get; set; }
        public string CyNo { get; set; }
        public string Seq { get; set; }
    }
    public class WarehouseScanDto
    {
        public List<string> Label { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string Current_User { get; set; }
        public string Scan_Date { get; set; }

    }
}
