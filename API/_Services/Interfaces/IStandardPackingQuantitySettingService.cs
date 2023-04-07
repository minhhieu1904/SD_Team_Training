using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using SD3_API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IStandardPackingQuantitySettingService
    {
        Task<PaginationUtility<MS_Package>> GetAll(PaginationParam pagination,StandardPackingQuantitySettingParam param );
        Task<PaginationUtility<MS_Package>> Search(PaginationParam pagination, string text);
        Task<OperationResult> Create (MS_Package model);
        Task<OperationResult> Update (MS_Package model);
    }
}