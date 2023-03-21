using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class StandardPackingQuantityParam
    {
        public string PackageNo {get; set;}
        public decimal PackageQty {get; set;}
        
        
    }

    public class StandardPackingQuantityAddParam
    {
        public string Manuf { get; set; }
        public string PackageNo { get; set; }
        public decimal PackageQty { get; set; }
        
        
    }
}