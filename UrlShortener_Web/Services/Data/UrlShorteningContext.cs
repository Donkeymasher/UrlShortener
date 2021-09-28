using Microsoft.EntityFrameworkCore;
using UrlShortener.Services.Data.DatabaseModels;
using UrlShortener.Services.Data.Interfaces;

namespace UrlShortener.Services.Data
{
    public class UrlShorteningContext : DbContext, IUrlShorteningContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public UrlShorteningContext(DbContextOptions<UrlShorteningContext> options) : base(options) {}
    }
}
