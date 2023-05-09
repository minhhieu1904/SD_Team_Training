using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.PackingScan
{
    public class ViewDataPackingScan
    {
        public string Manuf { get; set; }
        public string SDat { get; set; }
        public string TrNo { get; set; }
        public string Shift { get; set; }
        public string Grade { get; set; }
        public string QRCodeID { get; set; }
        public string ManNo { get; set; }
        public string PurNo { get; set; }
        public short Serial { get; set; }
        public string Size { get; set; }
        public decimal Qty { get; set; }
        public string CrUsr { get; set; }
        public DateTime CrDay { get; set; }
        public string EndCod { get; set; }
        public string RModel { get; set; }
        public string style { get; set; }
        public string Article { get; set; }
        public string CyNo { get; set; }
        public string Bitnbr { get; set; }
        public string Seq { get; set; }
        public string PrintTime { get; set; }

    }
}