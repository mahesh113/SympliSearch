using SympliDevelopment.Api.Models;

namespace SympliDevelopment.Api.Interface
{
    public interface IGSearch
    {
        Task<GSResponse> Search(string url, string keywords);
    }
}
