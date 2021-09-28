using System;
using System.Collections.Generic;
using UrlShortener.Services.Data.DatabaseModels;

namespace UrlShortener.Services.Data.Repositories
{
    public interface IShortenedUrlRepository : IDisposable
    {
        ShortenedUrl Find(int id);
        ShortenedUrl InsertOrUpdate(ShortenedUrl entity);
    }
}