using Microsoft.EntityFrameworkCore;
using UrlShortner.Services.Data.DatabaseModels;
using UrlShortner.Services.Data.Interfaces;

namespace UrlShortner.Services.Data
{
    public class UrlShorteningContext : DbContext, IUrlShorteningContext
    {
        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        public UrlShorteningContext(DbContextOptions<UrlShorteningContext> options) : base(options) {}
    }
}
