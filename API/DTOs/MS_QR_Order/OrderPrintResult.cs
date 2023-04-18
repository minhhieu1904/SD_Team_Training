using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.MS_QR_Order
{
    public class OrderPrintResult : OrderDataItem
    {
        public string rmodel { set; get; }
        public short serial { set; get; }
        public string article { set; get; }
        public decimal qty { set; get; }
        public string bitnbr { set; get; }
        public string custid { get; set; }
        public string kind { get; set; }
        public string printDate { get => DateTime.Now.ToString("yyyy/MM/dd"); }
        public string qrCodeName { set; get; }
        public string update_by { get; set; }
        public string update_time { get => printDate; set => value = update_time; }
        public string remark { get; set; }
    }
}