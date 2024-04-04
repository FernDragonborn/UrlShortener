using Microsoft.EntityFrameworkCore;
using UrlShortener_Backend.Models;

namespace UrlShortener_Backend.DbContext;
public class UrlShortenerDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public UrlShortenerDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<UrlData> Urls { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer();
    }
}