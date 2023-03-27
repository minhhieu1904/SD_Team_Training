using API.Helpers.Params.Transaction;

namespace API.Dtos.Transaction.PickingScan
{
    public class PickingMainDto
    {
        public string manuf { get; set; }
        public string declaration_no { get; set; }
        public string invno { get; set; }
        public string iono { get; set; }
        public DateTime? ExpectStorageOutDate { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
        public decimal? ExpectQTY { get; set; }
        public decimal? ActualQTY { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? StorageOutDate { get; set; }
        public string status { get; set; }
        public string updated_by { get; set; }
        public DateTime? update_time { get; set; }
        public bool checkRelease { get; set; }
    }



    public class GetScanPickingMainDto
    {
        public PickingMainDto pickingMain { get; set; }
        public List<PickingDetailDto> listPickingDetail { get; set; }
    }

    public class PickingUpdate
    {
        public GetScanPickingMainDto data { get; set; }
        public PickingScanParam param { get; set; }
    }

    public class ScanPickingExport : PickingDetailDto
    {
        public string invno { get; set; }
        public string ReleaseDate { get; set; }
    }

    public class DataExportExcel
    {
        public string sheetName { get; set; }
        public List<ScanPickingExport> listPickingDetail { get; set; }
    }
}