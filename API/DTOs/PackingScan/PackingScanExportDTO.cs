using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.PackingScan
{
    public class PackingScanExportDTO
    {
        public string ManNo { get; set; }
        public List<ViewDataPackingScan> ListItemPerPage { get; set;}
        public decimal Qty { get; set; }
    }
}