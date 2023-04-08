using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Report;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces.Report
{
    public interface IReport_Sort_Sumservice
    {
        Task<PaginationUtility<Report_Sort_SumParam>> GetData(PaginationParam pagination ,SortSumDTO param, bool isPaging = true);
        Task<List<getBrand>> GetBrand();
        Task<byte[]> ExportExcelDetails( SortSumDeltailDTOParam param);
    } 
}
