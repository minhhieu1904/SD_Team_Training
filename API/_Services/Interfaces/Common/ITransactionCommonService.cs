using SDCores;

namespace API._Services.Interfaces.Common
{
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface ITransactionCommonService
    {
        Task<List<KeyValuePair<string, string>>> GetListShift();
    }
}