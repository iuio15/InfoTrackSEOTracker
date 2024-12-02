using HtmlAgilityPack;
using InfoTrackSEOTracker.Core.Helpers;
using InfoTrackSEOTracker.Core.Interfaces;
using InfoTrackSEOTracker.Domain.Enums;
using System.Text.RegularExpressions;

namespace InfoTrackSEOTracker.Infrastructure.Scraper
{
    public class SearchScraper : ISearchScraper
    {
        private readonly HttpClient _httpClient;

        public SearchScraper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> ScrapeResultsAsync(string searchTerm, SearchEngine searchEngine)
        {
            var searchUrl = SearchEngineHelper.BuildSearchUrl(searchEngine, searchTerm);

            if (searchEngine == SearchEngine.Bing)
            {
                _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            }

            var response = await _httpClient.GetAsync(searchUrl);
            response.EnsureSuccessStatusCode();

            var html = await response.Content.ReadAsStringAsync();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var regexPattern = SearchEngineHelper.GetRegexPattern(searchEngine);

            var matches = Regex.Matches(html, regexPattern);

            var links = matches
                .Cast<Match>()
                .Select(match => match.Value)
                .Where(link => !string.IsNullOrWhiteSpace(link))
                .ToList();

            return links ?? new List<string>();
        }
    }
}