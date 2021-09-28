using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UrlShortner.Services.Data.Configuration;

namespace UrlShortner.Services.Data.Factories
{
    public class ContextFactory 
    {
        private readonly ConnectionStrings _connectionString;

        public ContextFactory(IOptions<ConnectionStrings> connectionStrings)
        {
            _connectionString = connectionStrings.Value;
        }

        public UrlShorteningContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<UrlShorteningContext>();
            optionsBuilder.UseSqlServer(_connectionString.UrlStore);

            return new UrlShorteningContext(optionsBuilder.Options);
        }
    }
}
