

using SDCores;

namespace API.Dtos.Transaction.OrderDataStatusAdjust
{
    public class OrderDataStatusAdjustDto
    {
        public string Brand { get; set; }
        public string MoldNumber { get; set; }
        public string Tolcls { get; set; }
        public string MaterialCodeBottom { get; set; }
        public string Size { get; set; }
        public string CodeOfCustomer { get; set; }
        public string PlanningNo { get; set; }
        public string PurchaseNo { get; set; }
        public string Status { get; set; }
        public string WkshNo { get; set; }
        public string NameOfCustomer { get; set; }
        public string TypeOfOrder { get; set; }
        public DateTime? ConfirmedDeliveryDate { get; set; }
        public string Article { get; set; }
        public decimal OrderQuantity { get; set; }
        public decimal WkshQty { get; set; }
        public decimal IncreaseOrderQuantity { get; set; }
        public decimal ReduceOrderQuantity { get; set; }
        public string AvailableQuantityForPrint { get; set; }
        public decimal PrintedQuantity { get; set; }
        public decimal CancelPritingQuantity { get; set; }
        public string MaterialCodeShoes { get; set; }
        public string ModelName { get; set; }
        public string ToolingSize { get; set; }
        public string Remark { get; set; }
        public DateTime TimeOfUpdating { get; set; }
        public string UpdatedBy { get; set; }
        public string prtno { get; set; }
    }

    public class OrderDataStatusAdjustResponse
    {
        public List<OrderDataStatusAdjustDto> AllData { get; set; }
        public PaginationUtility<OrderDataStatusAdjustDto> Pagination { get; set; }
    }

    public class OrderDataStatusAdjustParam
    {
        public string PlanningNo { get; set; }
        public string Purno { get; set; }
        public string Tolcls { get; set; }
    }

    public class OrderDataStatusAdjustUpdate
    {
        public string EndCod { get; set; } // trạng thái đơn hàng
        public List<OrderDataStatusAdjustDto> OrderDataStatusAdjusts { get; set; }
    }
}