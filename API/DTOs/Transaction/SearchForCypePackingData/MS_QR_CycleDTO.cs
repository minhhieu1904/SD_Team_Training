
namespace API.DTOs.Transaction.SearchForCyclePackingData
{
    public class MS_QR_CycleDTO
    {
        public string manuf { get; set; }
        public string manno { get; set; }
        public string seq { get; set; }
        public string purno { get; set; }
        public string article { get; set; }
        public string cyno { get; set; }        
        public string size { get; set; }
        public decimal qty { get; set; }
        public decimal pqty { get; set; }
        public decimal cqty { get; set; }
        public string endcod { get; set; }
        public DateTime update_time { get; set; }
        public string updated_by { get; set; }
        public string Biz_Key { get; set; }
    }
}