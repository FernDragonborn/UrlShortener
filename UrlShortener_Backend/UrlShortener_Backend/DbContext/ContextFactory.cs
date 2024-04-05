using Microsoft.EntityFrameworkCore;

namespace UrlShortener_Backend.DbContext;

public static class ContextFactory
{
    private static IConfiguration _config = new ConfigurationManager();

    private static DbContextOptions Options;

    public static void Initialize(IConfiguration config)
    {
        _config = config;
        Options = new DbContextOptionsBuilder<UrlShortenerDbContext>()
            .UseSqlServer(_config.GetConnectionString("Default"))
            .Options;
    }

    public static UrlShortenerDbContext CreateNew()
    {
        return new UrlShortenerDbContext(Options);
    }
}
