using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper.Params.ShiftDataMaintain
{
    public class StandardPackingQuantitySettingParam
    {
        public string Manuf { get; set; }

        public string PackageNo { get; set; }

        public decimal PackageQty { get; set; }
    }
}