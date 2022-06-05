using Microsoft.Extensions.Options;
using SympliDevelopment.Api.Interface;
using SympliDevelopment.Api.Models;
using SympliDevelopment.Api.Service;
using AutoMapper;

namespace SympliDevelopment.Api.SearchEngine
{
    public class GoogleSearch : ISearchEngine
    {
        private readonly GoogleSearchConfig _config;
        private readonly IMapper _mapper;
        private readonly HttpHandler _httpHandler;
        public GoogleSearch
            (IOptions<GoogleSearchConfig> options, 
            HttpHandler httpHandler,
            IMapper mapper)
        {
            _config = options.Value;
            _httpHandler = httpHandler;
            _mapper = mapper;
        }
        public async Task<GSResponse> Search(string url, string keywords)
        {
            Uri uri = new Uri(url);
            var resp = await _httpHandler.SendRequestAsync(uri);
            var ret = _mapper.Map<GSResponse>(resp);
            return ret;
        }
    }
}
