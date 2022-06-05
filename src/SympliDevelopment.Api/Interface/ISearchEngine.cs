using SympliDevelopment.Api.Models;

namespace SympliDevelopment.Api.Interface
{
    public interface ISearchEngine
    {
        Task<GSResponse> Search(string url, string keywords);
    }
}
