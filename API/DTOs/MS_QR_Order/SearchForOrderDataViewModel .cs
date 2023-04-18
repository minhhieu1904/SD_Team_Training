using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MS_QR_Order
{
    public class SearchForOrderDataViewModel : MS_QR_OrderDTO
    {
         public decimal available_quantity_for_Print { get => (wkshqty - pqty); }
        public bool isChecked { get => false;  set => value = isChecked; }
        public bool isDisabled { get => pqty >= wkshqty ; }
    }
}