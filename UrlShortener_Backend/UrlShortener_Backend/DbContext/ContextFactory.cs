using Microsoft.EntityFrameworkCore;

namespace UrlShortener_Backend.DbContext;

public static class ContextFactory
{
    private static IConfiguration _config = new ConfigurationManager();

    private static readonly DbContextOptions Options = new DbContextOptionsBuilder<UrlShortenerDbContext>()
        .UseSqlServer(_config["DbConnectionString"])
        .Options;

    public static void Initialize(IConfiguration config)
    {
        _config = config;
    }

    public static UrlShortenerDbContext CreateNew()
    {
        return new UrlShortenerDbContext(Options);
    }
}
