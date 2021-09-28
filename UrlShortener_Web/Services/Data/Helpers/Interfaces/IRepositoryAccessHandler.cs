using UrlShortner.Services.Data.Repositories;

namespace UrlShortener.Services.Data.Helpers
{
    public interface IRepositoryAccessHandler
    {
        public IShortenedUrlRepository AccessShortenedUrls();
    }
}
