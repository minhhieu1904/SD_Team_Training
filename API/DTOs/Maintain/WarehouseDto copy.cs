namespace API.DTOs.Maintain
{
    public class WarehouseDto
    {
        public string Manuf { get; set; }
        public string StoreH { get; set; }
        public string LocationName { get; set; }
    }

    public class WarehouseParam
    {
        public string StoreH { get; set; }
        public string LocationName { get; set; }
    }
}