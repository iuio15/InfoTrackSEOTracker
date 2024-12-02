using InfoTrackSEOTracker.Domain.Entities;

namespace InfoTrackSEOTracker.Core.Interfaces
{
    public interface ISearchResultRepository
    {
        Task SaveSearchResultAsync(SearchResult searchResult);
        Task<IEnumerable<SearchResult>> GetSearchResultsAsync();
    }
}