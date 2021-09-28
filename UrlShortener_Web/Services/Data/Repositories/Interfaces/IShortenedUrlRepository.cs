using System;
using System.Collections.Generic;
using UrlShortner.Services.Data.DatabaseModels;

namespace UrlShortner.Services.Data.Repositories
{
    public interface IShortenedUrlRepository : IDisposable
    {
        ShortenedUrl Find(int id);
        ShortenedUrl Find(string url);
        List<ShortenedUrl> Get();
        ShortenedUrl InsertOrUpdate(ShortenedUrl entity);
    }
}