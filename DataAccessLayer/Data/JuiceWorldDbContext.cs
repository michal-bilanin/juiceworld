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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Seed();

        // Product to Manufacturer (Many-to-One)
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Manufacturer)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.ManufacturerId)
            .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of Manufacturer if there are Products

        // CartItem to Product (Many-to-One)
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete CartItems if Product is deleted

        // CartItem to User (Many-to-One)
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete CartItems if User is deleted

        // CartItem to Order (Optional Many-to-One)
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Order)
            .WithMany(o => o.CartItems)
            .HasForeignKey(ci => ci.OrderId)
            .IsRequired(false) // Optional relationship
            .OnDelete(DeleteBehavior.Cascade); // Set OrderId to null if Order is deleted

        // Review to Product (Many-to-One)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete Reviews if Product is deleted

        // Review to User (Many-to-One)
        modelBuilder.Entity<Review>()
            .HasOne(r => r.User)
            .WithMany(u => u.Reviews)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete Reviews if User is deleted

        // WishListItem to Product (Many-to-One)
        modelBuilder.Entity<WishListItem>()
            .HasOne(wli => wli.Product)
            .WithMany(p => p.WishListItems)
            .HasForeignKey(wli => wli.ProductId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete WishListItems if Product is deleted

        // WishListItem to User (Many-to-One)
        modelBuilder.Entity<WishListItem>()
            .HasOne(wli => wli.User)
            .WithMany(u => u.WishListItems)
            .HasForeignKey(wli => wli.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete WishListItems if User is deleted

        // Address to User (Many-to-One)
        modelBuilder.Entity<Address>()
            .HasOne(a => a.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete Addresses if User is deleted

        // Order to User (Many-to-One)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete Orders if User is deleted

        // Order to Address (Many-to-One)
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Address)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.AddressId)
            .OnDelete(DeleteBehavior.Restrict); // Restrict deletion of Address if linked to Orders

        base.OnModelCreating(modelBuilder);
    }
}