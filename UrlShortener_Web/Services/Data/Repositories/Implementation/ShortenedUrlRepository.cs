using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UrlShortener.Services.Data.DatabaseModels;

namespace UrlShortener.Services.Data.Repositories
{
    public class ShortenedUrlRepository : IShortenedUrlRepository
    {
        private readonly UrlShorteningContext context;

        public ShortenedUrlRepository(UnitOfWork uow)
        {
            context = uow.Context;
        }

        public ShortenedUrl Find(int id)
        {
            return context.ShortenedUrls
                .FirstOrDefault(x => x.ShortenedUrlId == id);
        }

        public ShortenedUrl Find(string url)
        {
            return context.ShortenedUrls
                .FirstOrDefault(x => x.OriginalUrl == url);
        }

        public ShortenedUrl InsertOrUpdate(ShortenedUrl entity)
        {
            if (entity.ShortenedUrlId == default)
            {
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.ShortenedUrls.Add(entity);
                context.Entry(entity).State = EntityState.Modified;
            }

            context.SaveChanges();
            return entity;
        }

        public void Dispose()
        {
            context.Dispose();
        }

    }
}
