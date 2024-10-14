using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Data;

/**
 * The database context for the JuiceWorld database.
 */
public class JuiceWorldDbContext(DbContextOptions<JuiceWorldDbContext> options)
    : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        // TODO: add entity relationship configurations here

        base.OnModelCreating(modelBuilder);
    }
}