using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Transaction.PackingScan
{
    public class PackingScanViewDTO
    {       
        public DateTime transferFormNo { get; set; }
        public string manNo { get; set; }
        public string purNo { get; set; }
        public string bitnbr { get; set; }
        public string style { get; set; }
        public string article { get; set; }
        public string seq { get; set; }
        public string cyNo { get; set; }
        public short serial { get; set; }
        public string size { get; set; }
        public string crUsr { get; set; }
        public string endCod { get; set; }
        public string manuf { get; set; }
        public string RModel { get; set; }
        public DateTime crDay { get; set; }
        public string SDat { get; set; }
        public string Shift { get; set; }
        public string TrNo { get; set; }
        public string Grade { get; set; }
        public string QRCodeID { get; set; }
        public string PrintTime { get; set; }
        public decimal label_Qty { get; set; }
       
    }
}