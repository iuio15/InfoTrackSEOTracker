using InfoTrackSEOTracker.Domain.Enums;

namespace InfoTrackSEOTracker.Core.Interfaces
{
    public interface ISearchScraper
    {
        Task<List<string>> ScrapeResultsAsync(string keyword, SearchEngine searchEngine);
    }
}