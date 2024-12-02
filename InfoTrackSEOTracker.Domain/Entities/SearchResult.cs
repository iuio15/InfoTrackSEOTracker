using InfoTrackSEOTracker.Domain.Enums;

namespace InfoTrackSEOTracker.Domain.Entities
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Keyword { get; set; }
        public string Url { get; set; }
        public SearchEngine SearchEngine { get; set; }
        public string Positions { get; set; }
        public DateTime Timestamp { get; set; }
    }
}