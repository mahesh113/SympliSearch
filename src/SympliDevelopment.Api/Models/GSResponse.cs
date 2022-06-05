namespace SympliDevelopment.Api.Models
{
    public class GSResponse
    {
        public string Kind { get; set; }
        public object Url { get; set; }
        public QueryType Queries { get; set; }
        public object Context { get; set; }
        public object SearchInformation { get; set; }
        public List<ItemObject> Items { get; set; }
    }

    public class PageResult
    {
        public string Title { get; set; }
        public string TotalResults { get; set; }
        public string SearchTerms { get; set; }
        public string InputEncoding { get; set; }
        public string OutputEncoding { get; set; }
        public string Safe { get; set; }
        public string Cx { get; set; }
        public string LowRange { get; set; }
        public string HighRange { get; set; }
        public int Count { get; set; }
        public int StartIndex { get; set; }
    }
    public class QueryType
    {
        public List<PageResult> Request { get; set; }
        public List<PageResult> NextPage { get; set; }
        public List<PageResult> PreviousPage { get; set; }
    }
    public class ItemObject
    {
        public string Kind { get; set; }
        public string Title { get; set; }
        public string HtmlTitle { get; set; }
        public string Link { get; set; }
        public string DisplayLink { get; set; }
        public string Snippet { get; set; }
        public string HtmlSnippet { get; set; }
        public string CacheId { get; set; }
        public string FormattedUrl { get; set; }
        public string HtmlFormattedUrl { get; set; }
        public object PageMap { get; set; }

    }
}
