using SympliDevelopment.Api.Models;

namespace SympliDevelopment.Api.Interface
{
    public interface IGSearch
    {
        Task<string> Search(string url, string keywords);
    }
}
