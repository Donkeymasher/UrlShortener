using Microsoft.EntityFrameworkCore.Internal;
using UrlShortner.Services.Data;
using UrlShortner.Services.Data.Factories;
using UrlShortner.Services.Data.Repositories;

namespace UrlShortener.Services.Data.Helpers
{
    public class RepositoryAccessHandler : IRepositoryAccessHandler
    {
        private static ContextFactory DbContextFactory;

        public RepositoryAccessHandler(ContextFactory dbContextFactory)
        {
            DbContextFactory = dbContextFactory;
        }

        public IShortenedUrlRepository AccessShortenedUrls()
        {
            return new ShortenedUrlRepository(new UnitOfWork(DbContextFactory));
        }
    }
}
