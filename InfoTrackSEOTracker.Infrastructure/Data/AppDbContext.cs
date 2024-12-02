using InfoTrackSEOTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InfoTrackSEOTracker.Core.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SearchResult> SearchResults { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SearchResult>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Keyword).IsRequired();
                entity.Property(e => e.Url).IsRequired();
                entity.Property(e => e.Positions).IsRequired();
                entity.Property(e => e.SearchEngine).HasConversion<string>().IsRequired();
                entity.Property(e => e.Timestamp).IsRequired();
            });
        }
    }
}