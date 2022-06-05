namespace SympliDevelopment.Api.Service
{
        /// <summary>
        /// The HttpHandler class in not only used for Google search but can be 
        /// used for other Search services as well. It is not hard wired with Google custom search.
        /// </summary>
    public class HttpHandler
    {
        private readonly HttpClient _httpClient;
        public HttpHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<string> SendRequestAsync(Uri uri)
        {
            _httpClient.BaseAddress = uri;
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            using var httpResponseMessage = await _httpClient.GetAsync(uri);
            httpResponseMessage.EnsureSuccessStatusCode();
            var content = await httpResponseMessage.Content.ReadAsStringAsync();

            return content;
        }
    }
}
