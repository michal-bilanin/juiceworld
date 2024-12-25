using Commons.Enums;
using JuiceWorld.Data;
using JuiceWorld.Entities;

namespace TestUtilities.Data;

public static class TestDataHelper
{
    public static List<User> GetTestUsers()
    {
        return
        [
            DataInitializer.CreateUser(1, "user@example.com", "user", "password", "interesting bio", UserRole.Customer),
            DataInitializer.CreateUser(2, "admin@example.com", "admin", "password", "interesting bio", UserRole.Admin)
        ];
    }

    public static List<Manufacturer> GetTestManufacturers()
    {
        return
        [
            new Manufacturer { Id = 1, Name = "MediPharma" },
            new Manufacturer { Id = 2, Name = "Royal Pharmaceuticals" },
            new Manufacturer { Id = 3, Name = "Liniment Pharmaceuticals" },
            new Manufacturer { Id = 4, Name = "Vermodje" },
            new Manufacturer { Id = 5, Name = "Balkan Pharmaceuticals" }
        ];
    }

    public static List<Product> GetTestProducts()
    {
        return
        [
            new Product
            {
                Id = 1,
                Name = "Anadrol (Oxymetholone)",
                ManufacturerId = 1,
                Price = 4199,
                Description = "100 tablets, each 50mg",
                UsageType = ProductUsageType.Oral
            },
            new Product
            {
                Id = 2,
                Name = "Anastrozole",
                ManufacturerId = 1,
                Price = 2399,
                Description = "30 tablets, each 1mg",
                UsageType = ProductUsageType.Oral
            },
            new Product
            {
                Id = 3,
                Name = "Anastrozole / Arimidex",
                ManufacturerId = 2,
                Price = 1899,
                Description = "30 tablets, each 1mg",
                UsageType = ProductUsageType.Oral
            },
            new Product
            {
                Id = 4,
                Name = "Anavar (Oxandrolone)",
                ManufacturerId = 4,
                Price = 2399,
                Description = "100 tablets, each 10mg",
                UsageType = ProductUsageType.Oral
            },
            new Product
            {
                Id = 5,
                Name = "Boldenone Undecylenate",
                ManufacturerId = 3,
                Price = 2099,
                Description = "250mg/ml - 10ml",
                UsageType = ProductUsageType.Injectable
            }
        ];
    }

    public static List<Address> GetTestAddresses(List<User> users)
    {
        return
        [
            new Address
            {
                Id = 1,
                UserId = users[0].Id,
                Name = "John Pork",
                HouseNumber = "123",
                Street = "Main Street",
                City = "New York",
                ZipCode = "10001",
                Country = "USA",
                Type = AddressType.Shipping
            },
            new Address
            {
                Id = 2,
                UserId = users[0].Id,
                Name = "John Pork",
                HouseNumber = "123",
                Street = "Main Street",
                City = "New York",
                ZipCode = "10001",
                Country = "USA",
                Type = AddressType.Billing
            },
            new Address
            {
                Id = 3,
                UserId = users[1].Id,
                Name = "Freakbob",
                HouseNumber = "321",
                Street = "Broadway",
                City = "New York",
                ZipCode = "10002",
                Country = "USA",
                Type = AddressType.Billing
            },
            new Address
            {
                Id = 4,
                UserId = users[1].Id,
                Name = "Freakbob",
                HouseNumber = "69",
                Street = "Bronx",
                City = "New York",
                ZipCode = "104",
                Country = "USA",
                Type = AddressType.Shipping
            }
        ];
    }

    public static List<Order> GetTestOrders(List<User> users)
    {
        return
        [
            new Order
            {
                Id = 1,
                UserId = users[0].Id,
                AddressId = 1,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Processing,
                DeliveryType = DeliveryType.Express,
                PaymentMethodType = PaymentMethodType.Bitcoin
            },
            new Order
            {
                Id = 2,
                UserId = users[0].Id,
                AddressId = 1,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Cancelled,
                DeliveryType = DeliveryType.Standard,
                PaymentMethodType = PaymentMethodType.Bitcoin
            },
            new Order
            {
                Id = 3,
                UserId = users[1].Id,
                AddressId = 4,
                CreatedAt = DateTime.Now.AddDays(-3),
                Status = OrderStatus.Delivered,
                DeliveryType = DeliveryType.Standard,
                PaymentMethodType = PaymentMethodType.Bitcoin,
                Departure = DateTime.Now.AddDays(-2),
                Arrival = DateTime.Now.AddDays(-1)
            },
            new Order
            {
                Id = 4,
                UserId = users[1].Id,
                AddressId = 4,
                CreatedAt = DateTime.Now,
                Status = OrderStatus.Pending,
                DeliveryType = DeliveryType.Express,
                PaymentMethodType = PaymentMethodType.Monero
            }
        ];
    }

    public static List<OrderProduct> GetTestOrderProducts(List<Order> orders, List<Product> products)
    {
        return
        [
            new OrderProduct
            {
                Id = 1,
                OrderId = orders[0].Id,
                ProductId = products[0].Id,
                Quantity = 1
            },
            new OrderProduct
            {
                Id = 2,
                OrderId = orders[0].Id,
                ProductId = products[1].Id,
                Quantity = 2
            },
            new OrderProduct
            {
                Id = 3,
                OrderId = orders[1].Id,
                ProductId = products[2].Id,
                Quantity = 1
            },
            new OrderProduct
            {
                Id = 4,
                OrderId = orders[2].Id,
                ProductId = products[3].Id,
                Quantity = 1
            },
            new OrderProduct
            {
                Id = 5,
                OrderId = orders[2].Id,
                ProductId = products[4].Id,
                Quantity = 1
            },
            new OrderProduct
            {
                Id = 6,
                OrderId = orders[3].Id,
                ProductId = products[0].Id,
                Quantity = 1
            }
        ];
    }

    public static List<CartItem> GetTestCartItems(List<User> users, List<Product> products)
    {
        return
        [
            new CartItem
            {
                Id = 1,
                UserId = users[0].Id,
                ProductId = products[0].Id,
                Quantity = 1
            },
            new CartItem
            {
                Id = 2,
                UserId = users[0].Id,
                ProductId = products[1].Id,
                Quantity = 2
            },
            new CartItem
            {
                Id = 3,
                UserId = users[1].Id,
                ProductId = products[2].Id,
                Quantity = 1
            },
            new CartItem
            {
                Id = 4,
                UserId = users[1].Id,
                ProductId = products[3].Id,
                Quantity = 1
            },
            new CartItem
            {
                Id = 5,
                UserId = users[1].Id,
                ProductId = products[4].Id,
                Quantity = 1
            }
        ];
    }

    public static List<Review> GetTestReviews(List<User> users, List<Product> products)
    {
        return
        [
            new Review
            {
                Id = 1,
                UserId = users[0].Id,
                ProductId = products[0].Id,
                Rating = 5,
                Body = "Great product!"
            },
            new Review
            {
                Id = 2,
                UserId = users[0].Id,
                ProductId = products[1].Id,
                Rating = 4,
                Body = "Good product!"
            },
            new Review
            {
                Id = 3,
                UserId = users[1].Id,
                ProductId = products[2].Id,
                Rating = 3,
                Body = "Average product!"
            },
            new Review
            {
                Id = 4,
                UserId = users[1].Id,
                ProductId = products[3].Id,
                Rating = 2,
                Body = "Bad product!"
            },
            new Review
            {
                Id = 5,
                UserId = users[1].Id,
                ProductId = products[4].Id,
                Rating = 1,
                Body = "Terrible product!"
            }
        ];
    }

    public static List<WishListItem> GetTestWishListItems(List<User> users, List<Product> products)
    {
        return
        [
            new WishListItem
            {
                Id = 1,
                UserId = users[0].Id,
                ProductId = products[0].Id
            },
            new WishListItem
            {
                Id = 2,
                UserId = users[0].Id,
                ProductId = products[1].Id
            },
            new WishListItem
            {
                Id = 3,
                UserId = users[1].Id,
                ProductId = products[2].Id
            },
            new WishListItem
            {
                Id = 4,
                UserId = users[1].Id,
                ProductId = products[3].Id
            },
            new WishListItem
            {
                Id = 5,
                UserId = users[1].Id,
                ProductId = products[4].Id
            }
        ];
    }
}
