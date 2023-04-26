using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.PackingScan
{
    public class PackingScanDTOParam
    {
        public string shift { get; set; }
        public List<string> ListData{get;set;}
    }
}