using InfoTrackSEOTracker.Application.Services;
using InfoTrackSEOTracker.Core.Data;
using InfoTrackSEOTracker.Core.Interfaces;
using InfoTrackSEOTracker.Infrastructure.Repositories;
using InfoTrackSEOTracker.Infrastructure.Scraper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<ISearchScraper, SearchScraper>();
builder.Services.AddScoped<ISearchResultRepository, SearchResultRepository>();

// Add HttpClient for ISearchScraper
builder.Services.AddHttpClient<ISearchScraper, SearchScraper>();

// Add Authorization services
builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
app.Run();