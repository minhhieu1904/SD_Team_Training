using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.SearchForCyclePackingData
{
    public class SearchForCyclePackingDataViewModel : MS_QR_CycleDTO
    {
        public decimal available_quantity_for_Print { get => (qty - pqty); }
        public bool isChecked { get => false;  set => value = isChecked; }
        public bool isDisabled { get => pqty >= qty ; }
    }
}