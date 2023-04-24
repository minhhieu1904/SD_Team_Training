using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.report.QRCodeWIPReport
{
    public class CheckSumMissHeaderDTO
    {
        [StringLength(6)]
        public string yymmdd{get;set;}
        public decimal? Sumpackage { get; set; }
        public decimal? Sumpairs { get; set; }
        public decimal? SumSortPack { get; set; }
        public decimal? SumSortPairs { get; set; }
        public decimal? SumlossPairs { get; set; }
        public decimal? SumStoragePack { get; set; }
        public decimal? SumStoragePairs { get; set; }
        public decimal? MissPackage { get; set; }
        public decimal? MissPairs { get; set; }
        public decimal? Proportion { get; set; }
    }
    public class CheckSumMissDetailDTO{
        [StringLength(6)]
        public string yymmdd {get;set;}
        public string Manuf {get;set;}
        public string Type {get;set;}
        public string Manno {get;set;}
        public string Purno {get;set;}
        public string prtno {get;set;}
        public string wkshno {get;set;}
        public string QRCodeID {get;set;}
        public string rmodel {get;set;}
        public string tolcls {get;set;}
        public string bitnbr {get;set;}
        public DateTime? eta {get;set;}
        public string empno {get;set;}
        public short? serial {get;set;}
        public string size {get;set;}
        public decimal? qty {get;set;}
        public string Crusr {get;set;}
        public DateTime? Crday {get;set;}
        public string CrdaY_str { get => Crday.HasValue ? Crday.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; }
        public string softFlag {get;set;}
        public string storageFlag {get;set;}
    }

}