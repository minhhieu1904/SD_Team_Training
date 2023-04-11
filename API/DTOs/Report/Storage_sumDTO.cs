
namespace API.DTOs.Report
{
    public class Storage_sumDTO
    {    
        public DateTime? CrDay { get; set; }
        public string date_kind{ get; set; }
        public DateTime?date_start{ get; set; }
        public DateTime?date_end{ get; set; }
        public string brandname { get; set; }
        public string cusna { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string bitnbr { get; set; }
        public string kind { get; set; }
        public DateTime? eta_start { get; set; }
        public DateTime? eta_end { get; set; }
        public string size { get; set; }

    }
     public class StorageSumDeltailDTO
    {
        public string IsStorageSort{ get; set; }
        public DateTime? CrDay { get; set; }
        public string brandname { get; set; }
        public string QRCodeID { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
        public short serial { get; set; }
        public decimal storage_Qty { get; set; }
        public string cusid { get; set; }
        public string cusna { get; set; }
        public string rmodel { get; set; }
        public string tolcls { get; set; }
        public string ritnbr { get; set; }
        public string bitnbr { get; set; }
        public string article { get; set; }
        public string kind { get; set; }
        public DateTime? eta { get; set; }
    }
       public class StorageSumDeltailDTOParam
    { 
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
    }
}