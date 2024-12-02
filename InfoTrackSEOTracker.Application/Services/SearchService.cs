using InfoTrackSEOTracker.Domain.Entities;
using InfoTrackSEOTracker.Core.Interfaces;
using InfoTrackSEOTracker.Core.Models;  
using InfoTrackSEOTracker.Domain.Enums;

namespace InfoTrackSEOTracker.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchResultRepository _repository;
        private readonly ISearchScraper _scraper;

        public SearchService(ISearchResultRepository repository, ISearchScraper scraper)
        {
            _repository = repository;
            _scraper = scraper;
        }

        public async Task<SearchResponse> PerformSearchAsync(string keyword, string url, SearchEngine searchEngine)
        {
            var scrapedResults = await _scraper.ScrapeResultsAsync(keyword, searchEngine);

            List<int> ranks = new List<int>();
            int rank = 1;

            foreach (var scrapedResult in scrapedResults)
            {
                if (scrapedResult.Contains(url, StringComparison.OrdinalIgnoreCase))
                {
                    ranks.Add(rank);
                }
                rank++;
            }

            ranks = ranks.Any() ? ranks : new List<int> { 0 };

            string positions = string.Join(",", ranks.Select(i => i.ToString()));

            var searchResult = new SearchResult
            {
                Keyword = keyword,
                Url = url,
                Timestamp = DateTime.UtcNow,
                SearchEngine = searchEngine,
                Positions = positions
            };

            await _repository.SaveSearchResultAsync(searchResult);

            return new SearchResponse
            {
                Keyword = keyword,
                Url = url,
                Positions = positions,
                SearchEngine = searchEngine.ToString(),
                Timestamp = searchResult.Timestamp
            };
        }

        public async Task<IEnumerable<SearchResponse>> GetSearchHistoryAsync()
        {
            return (await _repository.GetSearchResultsAsync())
                .Select(r => new SearchResponse
                {
                    Keyword = r.Keyword,
                    Url = r.Url,
                    Positions = r.Positions,
                    SearchEngine = r.SearchEngine.ToString(),
                    Timestamp = r.Timestamp
                });
        }
    }
}