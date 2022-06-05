namespace SympliDevelopment.Api.Models
{
    public class GoogleSearchConfig
    {
        public const string ConfigVarName = "GoogleSettings";
        public string Name { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }
        public string SearchEngineId { get; set; }
    }
}
