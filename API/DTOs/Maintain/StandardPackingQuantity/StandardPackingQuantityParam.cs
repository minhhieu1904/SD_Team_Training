using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Maintain.StandardPackingQuantity
{
    public class StandardPackingQuantityParam
    {
        public string manuf { get; set; }
        public string packageNo {get;set;}
        public decimal packageQty {get; set;}
    }
}