using InfoTrackSEOTracker.Domain.Enums;
using System.Web;

namespace InfoTrackSEOTracker.Core.Helpers
{
    public static class SearchEngineHelper
    {
        public static string GetBaseUrl(SearchEngine searchEngine)
        {
            return searchEngine switch
            {
                SearchEngine.Google => "http://www.google.co.uk/search",
                SearchEngine.Bing => "http://www.bing.com/search",
                _ => throw new ArgumentOutOfRangeException(nameof(searchEngine), "Unsupported search engine.")
            };
        }

        public static string GetResultCountParam(SearchEngine searchEngine)
        {
            return searchEngine switch
            {
                SearchEngine.Google => "num=100",
                SearchEngine.Bing => "count=100",
                _ => throw new ArgumentOutOfRangeException(nameof(searchEngine), "Unsupported search engine.")
            };
        }

        public static string BuildSearchUrl(SearchEngine searchEngine, string searchTerm)
        {
            return $"{GetBaseUrl(searchEngine)}?q={HttpUtility.UrlEncode(searchTerm)}&{GetResultCountParam(searchEngine)}";
        }

        public static string GetRegexPattern(SearchEngine searchEngine)
        {
            return searchEngine switch
            {
                SearchEngine.Google => @"(?<=<div class=""egMi0 kCrYT""><a href=""/url\?q=)[^""]*",
                SearchEngine.Bing => @"<h2[^>]*>\s*<a[^>]*href=""(https?://[^""]+)""",
                _ => throw new ArgumentOutOfRangeException(nameof(searchEngine), "Unsupported search engine.")
            };
        }
    }
}