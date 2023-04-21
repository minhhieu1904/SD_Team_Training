namespace API._Services.Interfaces.Common
{
    public interface I_CommonServices
    {
        Task<List<KeyValuePair<string, string>>> GetListBrandName();
        Task<List<KeyValuePair<decimal, decimal>>> GetListPackage();
        Task<List<KeyValuePair<string, string>>> GetListStatus();
    }
}