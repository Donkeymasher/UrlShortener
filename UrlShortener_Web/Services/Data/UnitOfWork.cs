using System;
using UrlShortener.Services.Data.Factories;

namespace UrlShortener.Services.Data
{
    public class UnitOfWork : IDisposable
    {
        public UnitOfWork(ContextFactory contextFactory)
        {
            Context = contextFactory.CreateDbContext();
        }

        public UnitOfWork(UrlShorteningContext context)
        {
            Context = context;
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public UrlShorteningContext Context { get; }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
