using InfoTrackSEOTracker.Core.DTOs;
using InfoTrackSEOTracker.Core.Interfaces;
using InfoTrackSEOTracker.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackSEOTracker.Api.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("perform")]
        public async Task<IActionResult> PerformSearch([FromBody] SearchRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Keyword) || string.IsNullOrWhiteSpace(request.Url))
            {
                return BadRequest("Invalid input data. Please provide a valid keyword and URL");
            }

            try
            {
                var result = await _searchService.PerformSearchAsync(request.Keyword, request.Url, Enum.Parse<SearchEngine>(request.SearchEngine));
                if (result == null)
                {
                    return BadRequest("Search failed due to an internal issue.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while performing the search: {ex.Message}");
            }
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetSearchHistory()
        {
            try
            {
                var history = await _searchService.GetSearchHistoryAsync();

                if (history == null || !history.Any())
                {
                    return NotFound("No search history found.");
                }

                return Ok(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request. Please try again later.");
            }
        }
    }
}