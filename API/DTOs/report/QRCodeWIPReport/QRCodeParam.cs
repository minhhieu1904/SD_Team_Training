using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.report.QRCodeWIPReport
{
    public class QRCodeParam
    {
        public string rmodel{get; set;}
        public string tolcls{get;set;}
        public DateTime? mdat_start {get;set;}
        public DateTime? mdat_end {get;set;}
    }
}