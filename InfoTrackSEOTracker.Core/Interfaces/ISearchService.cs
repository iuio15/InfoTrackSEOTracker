using InfoTrackSEOTracker.Core.Models;
using InfoTrackSEOTracker.Domain.Enums;

namespace InfoTrackSEOTracker.Core.Interfaces
{
    public interface ISearchService
    {
        Task<SearchResponse> PerformSearchAsync(string keyword, string url, SearchEngine searchEngine);
        Task<IEnumerable<SearchResponse>> GetSearchHistoryAsync();
    }
}