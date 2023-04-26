using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.SearchForCyclePackingData
{
    public class CyclePackingPrint
    {
        public List<CyclePackingDataItem> Items { get; set; }
        public decimal PackageQty { get; set; }
        public decimal PrintQty { get; set; }
        public bool Balance { get; set; }
        public string Empno { get; set; }
        public bool IsB_grade { get; set; }
        public string Grade { get => IsB_grade ? "B" : "A"; }
        public string UserName { get; set; }
    }
     public class CyclePackingDataItem
    {
        public string purno { get; set; }
        public string manno { get; set; }
        public string size { get; set; }
        public string wkshno { get; set; }
        public decimal wkshqty { get; set; }
        public string prtno { get; set; }
        public decimal max_qty_can_print { get; set; }
    }
      public class CompareQtyResult 
    {
        public decimal qty { get; set; }
        public decimal pqty { get; set; }
        public decimal printQty { get; set; }
    }

}