
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        IMSShift_Repository MSShift {get;}
        IMSLocation_Repository MSLocation {get;}
        IMSDepartment_Repository MSDepartmet {get;}
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}