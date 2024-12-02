namespace InfoTrackSEOTracker.Core.Models
{
    public class SearchResponse
    {
        public string Keyword { get; set; }
        public string Url { get; set; }
        public string Positions { get; set; }
        public string SearchEngine { get; set; }
        public DateTime Timestamp { get; set; }
    }
}