using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class SUsersServices : IUsersServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public SUsersServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> AddNew(Users model)
        {
            var item = await _repositoryAccessor.Users.FirstOrDefaultAsync(x => x.Account.Trim() == model.Account.Trim() && x.Name == model.Name);
            if (item != null)
                return new OperationResult(false);
            _repositoryAccessor.Users.Add(model);
            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<PaginationUtility<Users>> GetDataPaing(PaginationParam param, string account, string name)
        {
            var data = await _repositoryAccessor.Users.FindAll().ToListAsync();
            if (!string.IsNullOrEmpty(account))
                data = data.Where(x => x.Account == account).ToList();
            if (!string.IsNullOrEmpty(name))
                data = data.Where(x => x.Name.Contains(name)).ToList();
            return PaginationUtility<Users>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<Users> GetItem(string account, string name)
        {
            return await _repositoryAccessor.Users.FirstOrDefaultAsync( x => x.Account == account && x.Name == name);
        }

        public async Task<OperationResult> Update(Users model)
        {
            var item = await _repositoryAccessor.Users.FirstOrDefaultAsync(x => x.Account == model.Account && x.Name == model.Name);
            if (item == null)
                return new OperationResult(false);
            item.Password = model.Password;
            item.Email = model.Email;
            item.IsActive = model.IsActive;
            _repositoryAccessor.Users.Update(item);
            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
                return new OperationResult(false);
        }
    }
}