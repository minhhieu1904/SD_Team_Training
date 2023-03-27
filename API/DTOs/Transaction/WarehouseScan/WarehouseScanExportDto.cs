namespace API.DTOs.Transaction.WarehouseScan
{
    public class WarehouseScanExportDto
    {
        public string Key { get; set; }
        public int Qty { get; set; }
        public List<MS_QR_StorageDto> ListWarehouseScan { get; set; }
    }
}
