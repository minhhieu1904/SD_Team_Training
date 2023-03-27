using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.ReprintWarehouseScan
{
    public class ReprintWarehouseScanDto
    {
        public DateTime SDat { get; set; }
        public string TrNo { get; set; }
        public decimal StorageQty { get; set; }
        public string StoreH { get; set; }
        public string Location { get; set; }
        public string ParNo { get; set; }
        public string Department { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public string Size { get; set; }
        public short Serial { get; set; }
        public decimal PQty { get; set; }
        public string WkshNo { get; set; }
        public decimal WkshQty { get; set; }
        public string PrtNo { get; set; }
        public DateTime MDat { get; set; }
        public string BitNbr { get; set; }
        public string RModel { get; set; }
    }

    public class ReprintWarehouseScanParam
    {
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public string trno { get; set; }
        public string location { get; set; }
        public string department { get; set; }
        public string manno { get; set; }
        public string purno { get; set; }
        public string size { get; set; }
    }
}