using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace JuiceWorld.Data;

/**
 * The database context for the JuiceWorld database.
 */
public class JuiceWorldDbContext(DbContextOptions<JuiceWorldDbContext> options, IConfiguration configuration)
    : DbContext(options)
{
    public const string ConnectionStringKey = "JuiceWorldDb";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringKey);
        optionsBuilder
            .UseNpgsql(connectionString)
            .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
            .UseLazyLoadingProxies();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        // TODO: add entity relationship configurations here

        base.OnModelCreating(modelBuilder);
    }
}