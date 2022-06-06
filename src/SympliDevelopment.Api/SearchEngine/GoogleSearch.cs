using Microsoft.Extensions.Options;
using SympliDevelopment.Api.Interface;
using SympliDevelopment.Api.Models;
using SympliDevelopment.Api.Service;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft;
using System.Text;
using SympliDevelopment.Api.Extenstions;

namespace SympliDevelopment.Api.SearchEngine
{
    public class GoogleSearch : IGSearch
    {
        private readonly GoogleSearchConfig _gsConfig;
        private readonly HttpHandler _httpHandler;
        public GoogleSearch
            (IOptions<GoogleSearchConfig> options, 
            HttpHandler httpHandler)
        {
            _gsConfig = options.Value;
            _httpHandler = httpHandler;
        }
        /// <summary>
        /// Search on Google using the API key. Key is configured to search on entire web,
        /// and not just on the Google sites.
        /// </summary>
        /// <param name="url">url to be looked for in search results.</param>
        /// <param name="keywords">keywords to search on google.</param>
        /// <returns></returns>
        public async Task<string> Search(string url, string keywords)
        {
            //Uri uri = new Uri(_config.Url);
            string ret = "0";
            List<int> retList = new List<int>();
            var queryParams = new Dictionary<string, string>()
{
                {"key", _gsConfig.Key },
                {"cx", _gsConfig.SearchEngineId },
                {"q", keywords },
                {"start", "1" }
            };
            for(int i = 0; i<10;i++ )
            {
                int start = i * 10 + 1; // 1, 11, 21 .... 91
                queryParams["start"] = start.ToString();
                string _uri = QueryHelpers.AddQueryString(_gsConfig.Url, queryParams);
                Uri uri = new Uri(_uri);
                var resp = await _httpHandler.SendRequestAsync(uri);

                var allResults = JsonConvert.DeserializeObject <GSResponse>(resp);
                if (allResults == null || allResults.Items?.Count == 0)
                    break;
                retList.AddRange(FindLinkInResult(url, allResults, start));
            }
            if(retList.Count > 0)
                ret = JsonConvert.SerializeObject(retList);
            
            return ret.ToPlainString();
        }
        /// <summary>
        /// Find the results which are from link as in <paramref name="url"/>
        /// </summary>
        /// <param name="url">url to be search in <paramref name="allResults"/></param>
        /// <param name="allResults">List of results which contain search result data from search engine</param>
        /// <param name="offset">offset number or start index of search result pages</param>
        /// <returns></returns>
        private IEnumerable<int> FindLinkInResult(string url, GSResponse? allResults, int offset)
        {
            List<int> ret = new List<int>();
            int counter = offset;
            foreach (var item in allResults.Items)
            {
                if(item.DisplayLink.Contains(url)|| item.Link.Contains(url)
                    || url.Contains(item.DisplayLink))
                {
                    ret.Add(counter);
                }
                counter++;
            }
            return ret;
        }
    }
}
