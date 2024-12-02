using InfoTrackSEOTracker.Api.Controllers;
using InfoTrackSEOTracker.Core.DTOs;
using InfoTrackSEOTracker.Core.Interfaces;
using InfoTrackSEOTracker.Core.Models;
using InfoTrackSEOTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InfoTrackSEOTracker.Tests.IntegrationTests
{
    [TestFixture]
    public class SearchControllerTests
    {
        [Test]
        public async Task PerformSearch_ReturnsExpectedResult()
        {
            // Arrange
            var mockService = new Mock<ISearchService>();
            mockService
                .Setup(s => s.PerformSearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchEngine>()))
                .ReturnsAsync(new SearchResponse
                {
                    Keyword = "test",
                    Url = "www.test.com",
                    Positions = "1,3",
                    SearchEngine = "Google",
                    Timestamp = DateTime.UtcNow
                });

            var controller = new SearchController(mockService.Object);

            // Act
            var result = await controller.PerformSearch(new SearchRequest
            {
                Keyword = "test",
                Url = "www.test.com",
                SearchEngine = "Google"
            }) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var value = result.Value as SearchResponse;
            Assert.That(value, Is.Not.Null);
            Assert.That(value.Keyword, Is.EqualTo("test"));
            Assert.That(value.Url, Is.EqualTo("www.test.com"));
            Assert.That(value.Positions, Is.EqualTo("1,3"));
            Assert.That(value.SearchEngine, Is.EqualTo("Google"));
        }

        [Test]
        public async Task PerformSearch_ReturnsBadRequest_WhenInvalidRequest()
        {
            // Arrange
            var mockService = new Mock<ISearchService>();
            var controller = new SearchController(mockService.Object);

            // Act
            var result = await controller.PerformSearch(new SearchRequest
            {
                Keyword = "",
                Url = ""
            }) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("Invalid input data. Please provide a valid keyword and URL"));
        }

        [Test]
        public async Task PerformSearch_ReturnsBadRequest_WhenSearchEngineIsInvalid()
        {
            // Arrange
            var mockService = new Mock<ISearchService>();
            var controller = new SearchController(mockService.Object);

            // Act
            var result = await controller.PerformSearch(new SearchRequest
            {
                Keyword = "test",
                Url = "www.test.com",
                SearchEngine = "InvalidEngine"
            }) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("An error occurred while performing the search: Requested value 'InvalidEngine' was not found."));
        }

        [Test]
        public async Task PerformSearch_ReturnsInternalServerError_WhenServiceFails()
        {
            // Arrange
            var mockService = new Mock<ISearchService>();
            mockService
                .Setup(s => s.PerformSearchAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchEngine>()))
                .ThrowsAsync(new Exception("Service failed"));

            var controller = new SearchController(mockService.Object);

            // Act
            var result = await controller.PerformSearch(new SearchRequest
            {
                Keyword = "test",
                Url = "www.test.com",
                SearchEngine = "Google"
            }) as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("An error occurred while performing the search: Service failed"));
        }

        [Test]
        public async Task GetSearchHistory_ReturnsOk_WhenHistoryExists()
        {
            // Arrange
            var mockService = new Mock<ISearchService>();
            mockService
                .Setup(s => s.GetSearchHistoryAsync())
                .ReturnsAsync(new List<SearchResponse>
                {
                    new SearchResponse
                    {
                        Keyword = "test",
                        Url = "www.test.com",
                        Positions = "1,2",
                        SearchEngine = "Google",
                        Timestamp = DateTime.UtcNow
                    }
                });

            var controller = new SearchController(mockService.Object);

            // Act
            var result = await controller.GetSearchHistory() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var history = result.Value as List<SearchResponse>;
            Assert.That(history, Is.Not.Null);
            Assert.That(history.Count, Is.EqualTo(1));
            Assert.That(history[0].Keyword, Is.EqualTo("test"));
            Assert.That(history[0].Url, Is.EqualTo("www.test.com"));
            Assert.That(history[0].Positions, Is.EqualTo("1,2"));
            Assert.That(history[0].SearchEngine, Is.EqualTo("Google"));
        }

        [Test]
        public async Task GetSearchHistory_ReturnsNotFound_WhenHistoryIsEmpty()
        {
            // Arrange
            var mockService = new Mock<ISearchService>();
            mockService
                .Setup(s => s.GetSearchHistoryAsync())
                .ReturnsAsync(new List<SearchResponse>());

            var controller = new SearchController(mockService.Object);

            // Act
            var result = await controller.GetSearchHistory() as NotFoundObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(404));
            Assert.That(result.Value, Is.EqualTo("No search history found."));
        }
    }
}