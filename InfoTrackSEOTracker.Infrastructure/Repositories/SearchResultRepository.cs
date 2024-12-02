using InfoTrackSEOTracker.Core.Data;
using InfoTrackSEOTracker.Core.Interfaces;
using InfoTrackSEOTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackSEOTracker.Infrastructure.Repositories
{
    public class SearchResultRepository : ISearchResultRepository
    {
        private readonly AppDbContext _context;

        public SearchResultRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveSearchResultAsync(SearchResult searchResult)
        {
            _context.SearchResults.Add(searchResult);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SearchResult>> GetSearchResultsAsync()
        {
            return await _context.SearchResults.ToListAsync();
        }
    }
}