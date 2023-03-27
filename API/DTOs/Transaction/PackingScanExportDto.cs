
namespace API.Dtos.Transaction.PackingScan
{
    public class PackingScanExportDto
    {
        public string ManNo { get; set; }
        public List<PackingScanViewDto> ListItemPerPage { get; set; }
        public decimal Qty { get; set; }
    }
}