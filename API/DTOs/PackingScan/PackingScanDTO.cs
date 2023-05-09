using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.PackingScan
{
    public class PackingScanDTO
    {
        public string Shift { get; set; }
        public List<string> ListData { get; set; }
    }
}