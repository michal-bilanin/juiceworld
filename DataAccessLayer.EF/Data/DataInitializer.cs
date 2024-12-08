using Bogus;
using Commons.Enums;
using Commons.Utils;
using JuiceWorld.Entities;
using Microsoft.EntityFrameworkCore;

namespace JuiceWorld.Data;

/**
 * Class responsible for seeding the database with initial data.
 */
public static class DataInitializer
{
    private const int SeedNumber = 69420;

    private static readonly List<Manufacturer> Manufacturers =
    [
        new() { Id = 1, Name = "MediPharma" },
        new() { Id = 2, Name = "Royal Pharmaceuticals" },
        new() { Id = 3, Name = "Liniment Pharmaceuticals" },
        new() { Id = 4, Name = "Vermodje" },
        new() { Id = 5, Name = "Balkan Pharmaceuticals" },
        new() { Id = 6, Name = "Anfarm" },
        new() { Id = 7, Name = "Bayer" },
        new() { Id = 8, Name = "Novartis" },
        new() { Id = 9, Name = "Pfizer" },
        new() { Id = 10, Name = "Royal Pharmaceuticals" },
        new() { Id = 11, Name = "Galenika" },
        new() { Id = 12, Name = "Zambon" },
        new() { Id = 13, Name = "GlobalPharma" },
        new() { Id = 14, Name = "BM" },
        new() { Id = 15, Name = "Sport Pharmaceuticals" }
    ];

    private static readonly List<User> Users =
    [
        CreateUser(1, "user@example.com", "user", "password", "I am a steroid user!", UserRole.Customer),
        CreateUser(2, "admin@example.com", "admin", "password", "I am a steroid Admin!", UserRole.Admin)
    ];

    public static User CreateUser(int id, string email, string userName, string password, string bio, UserRole role)
    {
        var user = new User
        {
            Id = id,
            Email = email,
            UserName = userName,
            Bio = bio,
            UserRole = role,
            PasswordSalt = AuthUtils.GenerateSalt(),
            PasswordHashRounds = 10,
            PasswordHash = ""
        };

        user.PasswordHash = AuthUtils.HashPassword(password, user.PasswordSalt, user.PasswordHashRounds);
        return user;
    }


    private static List<User> GenerateUsers()
    {
        var userIds = Users.Count + 1;
        var faker = new Faker<User>()
            .UseSeed(SeedNumber)
            .RuleFor(u => u.Id, _ => userIds++)
            .RuleFor(u => u.Email, f => f.Internet.Email())
            .RuleFor(u => u.UserName, f => f.Internet.UserName())
            .RuleFor(u => u.Bio, f => f.Lorem.Sentence())
            .RuleFor(u => u.UserRole, f => f.PickRandom<UserRole>())
            .RuleFor(u => u.PasswordSalt, _ => AuthUtils.GenerateSalt())
            .RuleFor(u => u.PasswordHashRounds, _ => 10)
            .RuleFor(u => u.PasswordHash,
                (_, u) => AuthUtils.HashPassword("password", u.PasswordSalt, u.PasswordHashRounds));
        
        return faker.Generate(100);
    }

    private static List<Address> GenerateAddresses(List<User> users)
    {
        var addressIds = 1;
        var faker = new Faker<Address>()
            .UseSeed(SeedNumber)
            .RuleFor(a => a.Id, _ => addressIds++)
            .RuleFor(a => a.Name, f => f.Name.FullName())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Street, f => f.Address.StreetName())
            .RuleFor(a => a.HouseNumber, f => f.Address.BuildingNumber())
            .RuleFor(a => a.ZipCode, f => f.Address.ZipCode())
            .RuleFor(a => a.Country, f => f.Address.Country())
            .RuleFor(a => a.Type, f => f.PickRandom<AddressType>())
            .RuleFor(a => a.UserId, f => f.PickRandom(users).Id);

        return faker.Generate(150);
    }

    private static List<Order> GenerateOrders(List<User> users, List<Address> addresses)
    {
        var orderIds = 1;
        var faker = new Faker<Order>()
            .UseSeed(SeedNumber)
            .RuleFor(o => o.Id, _ => orderIds++)
            .RuleFor(o => o.DeliveryType, f => f.PickRandom<DeliveryType>())
            .RuleFor(o => o.Status, f => f.PickRandom<OrderStatus>())
            .RuleFor(o => o.UserId, f => f.PickRandom(users).Id)
            .RuleFor(o => o.AddressId, f => f.PickRandom(addresses).Id);

        return faker.Generate(1000);
    }

    private static List<CartItem> GenerateCartItems(List<User> users)
    {
        var cartItemIds = 1;
        var faker = new Faker<CartItem>()
            .UseSeed(SeedNumber)
            .RuleFor(c => c.Id, _ => cartItemIds++)
            .RuleFor(c => c.ProductId, f => f.Random.Int(1, 47))
            .RuleFor(c => c.UserId, f => f.PickRandom(users).Id)
            .RuleFor(c => c.Quantity, f => f.Random.Int(1, 10));

        return faker.Generate(500);
    }

    private static List<OrderProduct> GenerateOrderProducts(List<Order> orders)
    {
        var orderProductIds = 1;
        var faker = new Faker<OrderProduct>()
            .UseSeed(SeedNumber)
            .RuleFor(op => op.Id, _ => orderProductIds++)
            .RuleFor(op => op.OrderId, f => f.PickRandom(orders).Id)
            .RuleFor(op => op.ProductId, f => f.Random.Int(1, 47))
            .RuleFor(op => op.Quantity, f => f.Random.Int(1, 10));

        return faker.Generate(2000);
    }

    private static List<Review> GenerateReviews(List<User> users)
    {
        var reviewIds = 1;
        var faker = new Faker<Review>()
            .UseSeed(SeedNumber)
            .RuleFor(r => r.Id, _ => reviewIds++)
            .RuleFor(r => r.ProductId, f => f.Random.Int(1, 47))
            .RuleFor(r => r.UserId, f => f.PickRandom(users).Id)
            .RuleFor(r => r.Rating, f => f.Random.Int(1, 5))
            .RuleFor(r => r.Body, f => f.Lorem.Sentence());

        return faker.Generate(2000);
    }

    private static List<WishListItem> GenerateWishListItems(List<User> users)
    {
        var wishListItemIds = 1;
        var faker = new Faker<WishListItem>()
            .UseSeed(SeedNumber)
            .RuleFor(w => w.Id, _ => wishListItemIds++)
            .RuleFor(w => w.ProductId, f => f.Random.Int(1, 47))
            .RuleFor(w => w.UserId, f => f.PickRandom(users).Id);

        return faker.Generate(300);
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        var users = GenerateUsers();
        var addresses = GenerateAddresses(users);
        var orders = GenerateOrders(users, addresses);
        var cartItems = GenerateCartItems(users);
        var orderProducts = GenerateOrderProducts(orders);
        var reviews = GenerateReviews(users);
        var wishListItems = GenerateWishListItems(users);

        // Not generated (authentic) manufacturer and product data
        modelBuilder.Entity<Manufacturer>().HasData(Manufacturers);
        modelBuilder.Entity<Product>().HasData(ProductsSeedData.Products);

        // Fixed users for testing
        modelBuilder.Entity<User>().HasData(Users);

        // Generated data
        modelBuilder.Entity<User>().HasData(users);
        modelBuilder.Entity<Address>().HasData(addresses);
        modelBuilder.Entity<Order>().HasData(orders);
        modelBuilder.Entity<CartItem>().HasData(cartItems);
        modelBuilder.Entity<OrderProduct>().HasData(orderProducts);
        modelBuilder.Entity<Review>().HasData(reviews);
        modelBuilder.Entity<WishListItem>().HasData(wishListItems);
    }
}
