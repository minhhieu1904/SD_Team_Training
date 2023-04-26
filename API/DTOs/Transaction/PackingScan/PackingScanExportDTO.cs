using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.PackingScan
{
    public class PackingScanExportDTO
    {
        public string manNo { get;set;}
        public List<PackingScanViewDTO> ListItemPerPage {get;set;}
        public decimal Qty {get;set;}
    }
}