using System.Linq.Expressions;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Data;

/**
 * The database context for the JuiceWorld database.
 */
public class JuiceWorldDbContext(DbContextOptions<JuiceWorldDbContext> options)
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<WishListItem> WishListItems { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Deleted)
            {
                continue;
            }

            // Change the state from Deleted to Modified, and set the DeletedAt time
            entry.State = EntityState.Modified;
            entry.CurrentValues[nameof(BaseEntity.DeletedAt)] = DateTime.Now;
        }

        return base.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Deleted)
            {
                continue;
            }

            // Change the state from Deleted to Modified, and set the DeletedAt time
            entry.State = EntityState.Modified;
            entry.CurrentValues[nameof(BaseEntity.DeletedAt)] = DateTime.Now;
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Category)
            .HasConversion<string>();

        modelBuilder.Entity<Product>()
            .Property(p => p.UsageType)
            .HasConversion<string>();

        // Product -> Manufacturer
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer)
            .WithMany(manufacturer => manufacturer.Products)
            .HasForeignKey(p => p.ManufacturerId)
            .OnDelete(DeleteBehavior.Cascade);

        // CartItem -> Product
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(product => product.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // CartItem -> User
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.User)
            .WithMany(user => user.CartItems)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Review -> Product
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(product => product.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Review -> User
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(user => user.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // WishListItem -> Product
        modelBuilder.Entity<WishListItem>()
            .HasOne(wl => wl.Product)
            .WithMany(product => product.WishListItems)
            .HasForeignKey(wl => wl.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // WishListItem -> User
        modelBuilder.Entity<WishListItem>()
            .HasOne(wl => wl.User)
            .WithMany(user => user.WishListItems)
            .HasForeignKey(wl => wl.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Order -> User
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(user => user.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .Property(o => o.DeliveryType)
            .HasConversion<string>();

        modelBuilder.Entity<Order>()
            .Property(o => o.PaymentMethodType)
            .HasConversion<string>();

        // Order -> Address
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Address)
            .WithMany(address => address.Orders)
            .HasForeignKey(o => o.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderProduct -> Product
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(product => product.OrdersProducts)
            .HasForeignKey(op => op.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // OrderProduct -> Order
        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(order => order.OrderProducts)
            .HasForeignKey(op => op.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Address>()
            .Property(a => a.Type)
            .HasConversion<string>();

        // Address -> User
        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(user => user.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<User>()
            .Property(u => u.UserRole)
            .HasConversion<string>();

        // Global query filter for soft-deleted entities
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (!typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            {
                continue;
            }

            var parameter = Expression.Parameter(entityType.ClrType);
            var filter = Expression.Lambda(
                Expression.Equal(
                    Expression.Property(parameter, nameof(BaseEntity.DeletedAt)),
                    Expression.Constant(null)
                ),
                parameter
            );
            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
        }

        base.OnModelCreating(modelBuilder);

        modelBuilder.Seed();
    }
}
