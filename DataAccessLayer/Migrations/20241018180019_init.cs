using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JuiceWorld.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PasswordHashRounds = table.Column<int>(type: "integer", nullable: false),
                    PasswordSalt = table.Column<string>(type: "text", nullable: false),
                    UserRole = table.Column<string>(type: "text", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    UsageType = table.Column<string>(type: "text", nullable: false),
                    ManufacturerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    HouseNumber = table.Column<string>(type: "text", nullable: false),
                    ZipCode = table.Column<string>(type: "text", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishListItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishListItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishListItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DeliveryType = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Departure = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Arrival = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    PaymentMethodType = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MediPharma", null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Royal Pharmaceuticals", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Liniment Pharmaceuticals", null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Vermodje", null },
                    { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Balkan Pharmaceuticals", null },
                    { 6, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Anfarm", null },
                    { 7, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Bayer", null },
                    { 8, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Novartis", null },
                    { 9, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pfizer", null },
                    { 10, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Royal Pharmaceuticals", null },
                    { 11, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Galenika", null },
                    { 12, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Zambon", null },
                    { 13, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GlobalPharma", null },
                    { 14, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "BM", null },
                    { 15, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Sport Pharmaceuticals", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Bio", "CreatedAt", "DeletedAt", "Email", "PasswordHash", "PasswordHashRounds", "PasswordSalt", "UpdatedAt", "UserName", "UserRole" },
                values: new object[,]
                {
                    { 1, "I am a steroid user!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "user@example.com", "cFqhpGxzCmNzKJmZ1tRdIBI7dKoT+DTkDsY7QqpY7p4=", 10, "gHaq4V7fMxHSwLJvWa2I1w==", null, "user", "Customer" },
                    { 2, "I am a steroid Admin!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@example.com", "uctYSNmWbWy5rNMAVBi/BmMTbuANTKF2e9TJFvl3eaY=", 10, "Z0KqCJHfrcha+u7Mvju/Ag==", null, "admin", "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "DeletedAt", "HouseNumber", "Name", "Street", "Type", "UpdatedAt", "UserId", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Brno", "Czech Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "18", "Jozef Tringál", "Hrnčířská", "Shipping", null, 1, "60200" },
                    { 2, "Brno", "Czech Republic", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "18", "Jozef Tringál", "Hrnčířská", "Billing", null, 1, "60200" },
                    { 3, "Bratislava", "Slovakia", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "2", "Ignác Lakeť", "Malý trh", "Billing", null, 2, "81108" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "DeletedAt", "Description", "ImageUrl", "ManufacturerId", "Name", "Price", "UpdatedAt", "UsageType" },
                values: new object[,]
                {
                    { 1, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100 tablets, each 50mg", null, 1, "Anadrol (Oxymetholone)", 4199m, null, "Oral" },
                    { 2, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 1mg", null, 1, "Anastrozole", 2399m, null, "Oral" },
                    { 3, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 1mg", null, 2, "Anastrozole / Arimidex", 1899m, null, "Oral" },
                    { 4, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100 tablets, each 10mg", null, 4, "Anavar (Oxandrolone)", 2399m, null, "Oral" },
                    { 5, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "250mg/ml - 10ml", null, 3, "Boldenone Undecylenate", 2099m, null, "Injectable" },
                    { 6, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "250mg/ml - 10ml", null, 1, "Boldenone Undecylenate", 2399m, null, "Injectable" },
                    { 7, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "10mg", null, 1, "BPC-157", 2599m, null, "Injectable" },
                    { 8, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "20 tablets, each 1mg", null, 14, "Cabaser Cabergoline", 3299m, null, "Oral" },
                    { 9, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 20mg", null, 15, "Cialis Sex Med", 1899m, null, "Oral" },
                    { 10, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5mg", null, 1, "CJC1295 DAC", 2899m, null, "Injectable" },
                    { 11, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100 tablets, each 40mcg", null, 5, "Clenbuterol", 2399m, null, "Oral" },
                    { 12, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "24 tablets, each 50mg", null, 6, "Clomiphene Citrate", 1799m, null, "Oral" },
                    { 13, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "60 tablets, each 50mg", null, 1, "Clomipfene (Clomid)", 2199m, null, "Oral" },
                    { 14, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100 tablets, each 10mg", null, 5, "Dianabol (Methandienone)", 2099m, null, "Oral" },
                    { 15, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "270g (30 dávek)", null, 13, "DMAA Pre Workout Booster", 2599m, null, "Oral" },
                    { 16, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "200mg/ml - 10ml", null, 3, "Drostanolone Enanthate", 2199m, null, "Injectable" },
                    { 17, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100mg/ml - 10ml", null, 3, "Drostanolone Propionate", 2099m, null, "Injectable" },
                    { 18, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 25mg", null, 1, "Exemestane", 2399m, null, "Oral" },
                    { 19, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 25mg", null, 2, "Exemestane Aromasin", 2099m, null, "Oral" },
                    { 20, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "1mg", null, 1, "Follistatin 334", 2899m, null, "Injectable" },
                    { 21, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "10mg", null, 1, "GHRP-6", 2199m, null, "Injectable" },
                    { 22, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5000iu", null, 15, "HCG 1vial", 1999m, null, "Injectable" },
                    { 23, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5000iu", null, 1, "HCG", 2199m, null, "Injectable" },
                    { 24, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "5mg", null, 1, "HGH Fragment 176-191", 2399m, null, "Injectable" },
                    { 25, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100iu", null, 1, "HGH (Somatropin)", 12999m, null, "Injectable" },
                    { 26, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "1mg", null, 1, "IGF-1 LR3", 2899m, null, "Injectable" },
                    { 27, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 20mg", null, 2, "Isotretinoin Roaccutane", 1999m, null, "Oral" },
                    { 28, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "400mg/ml - 10ml", null, 1, "Mass 400 - Testo/Decamix (5:3)", 3299m, null, "Injectable" },
                    { 29, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100mg/ml - 10ml", null, 2, "Masteron Drostanolone Propionate", 2099m, null, "Injectable" },
                    { 30, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "200mg/ml - 10ml", null, 1, "Masterone Enanthate (Drosta-E)", 3899m, null, "Injectable" },
                    { 31, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "10mg", null, 1, "MT2", 2399m, null, "Injectable" },
                    { 32, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "250mg/ml - 10ml", null, 3, "Nandrolone Decanoate", 2099m, null, "Injectable" },
                    { 33, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100mg/ml - 10ml", null, 1, "Primobolan Enanthate", 3899m, null, "Injectable" },
                    { 34, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100 tablets, each 25mg", null, 2, "Proviron", 2099m, null, "Oral" },
                    { 35, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 100mg", null, 12, "Sildenafil", 2099m, null, "Oral" },
                    { 36, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "250mg/ml - 10ml", null, 1, "Sustanon 250", 2899m, null, "Injectable" },
                    { 37, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "60 tablets, each 20mg", null, 1, "Tamoxifen", 2099m, null, "Oral" },
                    { 38, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "60 tablets, each 20mg", null, 2, "Tamoxifen (Nolvadex)", 1799m, null, "Oral" },
                    { 39, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100 tablets, each 25mcg", null, 2, "T3 Cytomel", 1999m, null, "Oral" },
                    { 40, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "30 tablets, each 20mg", null, 1, "Tadalafil", 1799m, null, "Oral" },
                    { 41, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "250mg/ml - 10ml", null, 1, "Testosterone Cypionate", 2399m, null, "Injectable" },
                    { 42, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "250mg/ml - 10ml", null, 1, "Testosterone Enanthate", 2399m, null, "Injectable" },
                    { 43, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "250mg/ml - 10ml", null, 1, "Testosterone Mix Sustanon", 2899m, null, "Injectable" },
                    { 44, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100mg/ml - 10ml", null, 3, "Trenbolone Acetate", 2399m, null, "Injectable" },
                    { 45, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "200mg/ml - 10ml", null, 1, "Trenbolone Enanthate", 2599m, null, "Injectable" },
                    { 46, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "200mg/ml - 10ml", null, 1, "Trenbolone Mix", 2599m, null, "Injectable" },
                    { 47, "Testosterone", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "100 tablets, each 10mg", null, 1, "Turanabol (Chlorodehydromethyltestosterone)", 1999m, null, "Oral" }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "OrderId", "ProductId", "Quantity", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 1, 2, null, 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 2, 1, null, 1 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, 3, 3, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AddressId", "Arrival", "CreatedAt", "DeletedAt", "DeliveryType", "Departure", "PaymentMethodType", "Status", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Standard", null, "Monero", "Pending", null, 1 },
                    { 2, 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Express", null, "Monero", "Delivered", null, 1 },
                    { 3, 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Standard", null, "Monero", "Pending", null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "Body", "CreatedAt", "DeletedAt", "ProductId", "Rating", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Great product! 💪💪💪💪", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 5, null, 1 },
                    { 2, "Good product!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 4, null, 1 },
                    { 3, "Average product!", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, 3, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "WishListItems",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "ProductId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null, 1 },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1 },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 3, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "OrderId", "ProductId", "Quantity", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 9, 5, null },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, 3, 7, null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 8, 12, null },
                    { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, 1, 9, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserId",
                table: "Addresses",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_OrderId",
                table: "CartItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AddressId",
                table: "Orders",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ProductId",
                table: "Reviews",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_ProductId",
                table: "WishListItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishListItems_UserId",
                table: "WishListItems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "WishListItems");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
