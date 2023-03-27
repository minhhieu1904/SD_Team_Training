using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SDCores;

namespace API._Services.Interfaces.common
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface I_CommonServices
    {
        Task<List<KeyValuePair<string, string>>> GetListBrandName();
        Task<List<KeyValuePair<string, string>>> GetListLocationName();
        Task<List<KeyValuePair<string, string>>> GetListDepartmentName();
        Task<List<KeyValuePair<string, string>>> GetListShift();
        Task<List<KeyValuePair<string, string>>> GetListTolcls();
    }
}