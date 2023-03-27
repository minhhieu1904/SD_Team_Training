using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction
{
    public class PackingScanDto
    {
        public string Shift { get; set; }
        public string Scan_Date { get; set; }
        public List<string> ListData { get; set; }
    }
}