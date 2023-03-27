using API.Dtos.Commons;

namespace API.Dtos.Transaction.SearchForOrderData
{
    public class SearchForOrderDataViewModel : MS_QR_OrderDto
    {
        public decimal available_quantity_for_Print { get => (wkshqty - pqty); }
        public bool isChecked { get => false; set => value = isChecked; }
        public bool isDisabled { get => pqty >= wkshqty; }
    }
}