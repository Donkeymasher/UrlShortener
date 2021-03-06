using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UrlShortener.Services.Data.Configuration;

namespace UrlShortener.Services.Data.Factories
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
            optionsBuilder.UseSqlServer(_connectionString.SQLServer);

            return new UrlShorteningContext(optionsBuilder.Options);
        }
    }
}
