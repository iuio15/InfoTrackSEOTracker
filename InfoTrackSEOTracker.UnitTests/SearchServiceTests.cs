using InfoTrackSEOTracker.Application.Services;
using InfoTrackSEOTracker.Core.Interfaces;
using InfoTrackSEOTracker.Core.Models;
using InfoTrackSEOTracker.Domain.Entities;
using InfoTrackSEOTracker.Domain.Enums;
using Moq;

namespace InfoTrackSEOTracker.Tests.UnitTests
{
    [TestFixture]
    public class SearchServiceTests
    {
        [Test]
        public async Task PerformSearchAsync_ReturnsCorrectPositions()
        {
            // Arrange
            string keyword = "example keyword";
            string url = "www.infotrack.co.uk";

            var scraperMock = new Mock<ISearchScraper>();
            scraperMock
                .Setup(s => s.ScrapeResultsAsync(It.IsAny<string>(), It.IsAny<SearchEngine>()))
                .ReturnsAsync(new List<string>
                {
            "www.example.com",
            "www.infotrack.co.uk",
            "www.sample.com",
            "www.infotrack.co.uk"
                });

            var repositoryMock = new Mock<ISearchResultRepository>();
            var searchService = new SearchService(repositoryMock.Object, scraperMock.Object);

            // Act
            SearchResponse result = await searchService.PerformSearchAsync(keyword, url, SearchEngine.Google);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Positions, Is.EqualTo("2,4"));
            Assert.That(result.Keyword, Is.EqualTo(keyword));
            Assert.That(result.Url, Is.EqualTo(url));
            Assert.That(result.SearchEngine, Is.EqualTo(SearchEngine.Google.ToString()));
            Assert.That(result.Timestamp, Is.Not.EqualTo(DateTime.MinValue));
        }

        [Test]
        public async Task GetSearchHistoryAsync_ReturnsExpectedResults()
        {
            // Arrange
            var searchHistory = new List<SearchResult>
            {
                new SearchResult
                {
                    Keyword = "example keyword",
                    Url = "www.infotrack.co.uk",
                    Timestamp = DateTime.UtcNow,
                    Positions = "2,4",
                    SearchEngine = SearchEngine.Google
                }
            };

            var repositoryMock = new Mock<ISearchResultRepository>();
            repositoryMock
                .Setup(r => r.GetSearchResultsAsync())
                .ReturnsAsync(searchHistory);

            var scraperMock = new Mock<ISearchScraper>();
            var searchService = new SearchService(repositoryMock.Object, scraperMock.Object);

            // Act
            var result = (await searchService.GetSearchHistoryAsync()).ToList();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Keyword, Is.EqualTo("example keyword"));
            Assert.That(result[0].Url, Is.EqualTo("www.infotrack.co.uk"));
            Assert.That(result[0].Positions, Is.EqualTo("2,4"));
            Assert.That(result[0].SearchEngine, Is.EqualTo(SearchEngine.Google.ToString()));
        }
    }
}