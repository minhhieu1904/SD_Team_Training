namespace API._Services.Interfaces.Common
{
    public interface I_CommonService
    {
        Task<List<KeyValuePair<string, string>>> GetListBrandName();
    }
}