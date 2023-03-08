using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface I_ShiltDataMaintain
    {
        Task<List<MS_Shift>> GetData();
        Task<OperationResult> Addnew(MS_Shift model);
        Task<OperationResult> Update(MS_Shift model);

        Task<MS_Shift> GetDataOnly(string manuf, string shift);
        Task<OperationResult> Delete(MS_Shift model);

    }
}