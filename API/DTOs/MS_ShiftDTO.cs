using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.DTOs
{
    public class MS_ShiftDTO
    {
        public string Manuf { get; set; }
        public string Shift { get; set; }
        public string ShiftName { get; set; }
    }
}
