using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDCores;

namespace API._Services.Interfaces
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_SortNewReportByMidasProvideService
    {
        Task<OperationResult> ExportExcel(string sortDate, string name);
    }
}