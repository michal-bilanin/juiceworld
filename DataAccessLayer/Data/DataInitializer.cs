using System.Security.Cryptography;
using JuiceWorld.Constants;
using JuiceWorld.Entities;
using JuiceWorld.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace JuiceWorld.Data;

/**
 * Class responsible for seeding the database with initial data.
 */
public static class DataInitializer
{
    private static List<Manufacturer> _manufacturers =
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
        new() { Id = 15, Name = "Sport Pharmaceuticals" },
    ];

    private static List<User> _users =
    [
        CreateUser(1, "user@example.com", "user", "password", UserRole.Customer),
        CreateUser(2, "admin@example.com", "admin", "password", UserRole.Admin)
    ];

    private static List<Address> _addresses =
    [
        new()
        {
            Id = 1, Name = "Jozef Tringál", City = "Brno", Street = "Hrnčířská", HouseNumber = "18", ZipCode = "60200",
            Country = "Czech Republic", Type = AddressType.Shipping, UserId = 1
        },
        new()
        {
            Id = 2, Name = "Jozef Tringál", City = "Brno", Street = "Hrnčířská", HouseNumber = "18", ZipCode = "60200",
            Country = "Czech Republic", Type = AddressType.Billing, UserId = 1
        },
        new()
        {
            Id = 3, Name = "Ignác Lakeť", City = "Bratislava", Street = "Malý trh", HouseNumber = "2",
            ZipCode = "81108", Country = "Slovakia", Type = AddressType.Billing, UserId = 2
        },
    ];

    private static List<Order> _orders =
    [
        new()
        {
            Id = 1, DeliveryType = DeliveryType.Standard, Status = OrderStatus.Pending, UserId = 1, AddressId = 1
        },
        new()
        {
            Id = 2, DeliveryType = DeliveryType.Express, Status = OrderStatus.Delivered, UserId = 1, AddressId = 2
        },
        new()
        {
            Id = 3, DeliveryType = DeliveryType.Standard, Status = OrderStatus.Pending, UserId = 2, AddressId = 3
        },
    ];

    private static List<CartItem> _cartItems =
    [
        new() { Id = 1, ProductId = 1, UserId = 1, Quantity = 2 },
        new() { Id = 2, ProductId = 2, UserId = 1, Quantity = 1 },
        new() { Id = 3, ProductId = 3, UserId = 2, Quantity = 3 },
    ];

    private static List<OrderProduct> _orderProducts =
    [
        new() { Id = 1, OrderId = 1, ProductId = 9, Quantity = 5 },
        new() { Id = 2, OrderId = 1, ProductId = 3, Quantity = 7 },
        new() { Id = 3, OrderId = 2, ProductId = 8, Quantity = 12 },
        new() { Id = 3, OrderId = 2, ProductId = 1, Quantity = 9 },
    ];

    private static List<Review> _reviews =
    [
        new() { Id = 1, ProductId = 1, UserId = 1, Rating = 5, Body = "Great product! 💪💪💪💪" },
        new() { Id = 2, ProductId = 2, UserId = 1, Rating = 4, Body = "Good product!" },
        new() { Id = 3, ProductId = 3, UserId = 2, Rating = 3, Body = "Average product!" },
    ];

    private static List<WishListItem> _wishListItems =
    [
        new() { Id = 1, ProductId = 1, UserId = 1 },
        new() { Id = 2, ProductId = 2, UserId = 1 },
        new() { Id = 3, ProductId = 3, UserId = 2 },
    ];

    private static User CreateUser(int id, string email, string userName, string password, UserRole role)
    {
        var user = new User
        {
            Id = id,
            Email = email,
            UserName = userName,
            UserRole = role,
            PasswordSalt = AuthUtils.GenerateSalt(),
            PasswordHashRounds = 10
        };

        user.PasswordHash = AuthUtils.HashPassword(password, user.PasswordSalt);
        return user;
    }

    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>().HasData(_manufacturers);
        modelBuilder.Entity<Product>().HasData(ProductsSeedData.Products);

        modelBuilder.Entity<User>().HasData(_users);
        modelBuilder.Entity<Address>().HasData(_addresses);
        modelBuilder.Entity<Order>().HasData(_orders);
        modelBuilder.Entity<CartItem>().HasData(_cartItems);
        modelBuilder.Entity<OrderProduct>().HasData(_orderProducts);
        modelBuilder.Entity<Review>().HasData(_reviews);
        modelBuilder.Entity<WishListItem>().HasData(_wishListItems);
    }
}
