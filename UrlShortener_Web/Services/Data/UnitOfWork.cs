using System;
using UrlShortner.Services.Data.Factories;

namespace UrlShortner.Services.Data
{
    public class UnitOfWork : IDisposable
    {
        private readonly UrlShorteningContext _context;

        public UnitOfWork(ContextFactory contextFactory)
        {
            _context = contextFactory.CreateDbContext();
        }

        public UnitOfWork(UrlShorteningContext context)
        {
            _context = context;
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public UrlShorteningContext Context
        {
            get { return _context; }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
