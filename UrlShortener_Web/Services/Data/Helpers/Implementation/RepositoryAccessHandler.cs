using UrlShortener.Services.Data.Factories;
using UrlShortener.Services.Data;
using UrlShortener.Services.Data.Repositories;

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
