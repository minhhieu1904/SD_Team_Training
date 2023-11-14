using System.ComponentModel.DataAnnotations.Schema;

namespace API.Dtos.Transaction.WarehouseOutScan
{
    public class StorageOut
    { 
        public string iono { get; set; }
    }

    public class MS_QR_StorageDto
    {
        public string manuf { get; set; }
        public DateTime Sdat { get; set; }
        public DateTime iodat { get; set; }
        public string trno { get; set; }
        public string storeh { get; set; }
        public string parno { get; set; }
        public string Grade { get; set; }
        public string QRCodeID { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        [Column(TypeName = "decimal(5, 0)")]
        public decimal serial { get; set; }
        public string size { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal qty { get; set; }
        public string crusr { get; set; }
        public DateTime crday { get; set; }
        public string biz_flag { get; set; }
        public DateTime? biz_time { get; set; }
        public string wkshno { get; set; }
        public string prtno { get; set; }
        [Column(TypeName = "decimal(7, 1)")]
        public decimal wkshqty { get; set; }
        public DateTime mdat { get; set; }
        public string uscod { get; set; }
        public string empno { get; set; }
        public string bitnbr { get; set; }
        public string ritnbr { get; set; }
        public string IsPicking { get; set; }
        public string IsStorageOut { get; set; }
        public DateTime? StorageOutDate { get; set; }
        public DateTime? Odat { get; set; }
        public string action { get; set; }
        public bool isDelete { get; set; }
        public string iono { get; set; }
    }

    public class StorageOutExportData
    {
        public string iono { get; set; }
        public List<MS_QR_StorageDto> listStorageData { get; set; }
    }
    public class PickingDetailOutExportData
    {
        public string iono { get; set; }
        public List<MS_QR_PickingDetailDto> listData { get; set; }
    }

    public class MS_QR_PickingDetailDto
    {
        public string manuf { get; set; }
        public DateTime Sdat { get; set; }
        public DateTime? StorageOutDate { get; set; }
        public string DeclarationNo { get; set; }
        public string Invno { get; set; }
        public string iono { get; set; }
        public string Grade { get; set; }
        public string QRCodeID { get; set; }
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
        public string ritnbr { get; set; }
        public string bitnbr { get; set; }
    }
}