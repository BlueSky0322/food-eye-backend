using System;
using System.Collections.Generic;
using FoodEyeAPI.Models.Table;
using Microsoft.EntityFrameworkCore;

namespace FoodEyeAPI.Database;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
    public DbSet<Item> Items { get; set; }
    public DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed data for Users
        var user = new User
        {
            UserID = Guid.NewGuid().ToString(),
            Email = "a@a.a",
            Password = "123123",
            Name = "John Doe",
            Age = 30,
            Address = "123 Main Street",
            DateOfBirth = new DateTime(1993, 5, 15),
            UserRole = "Admin"
        };

        modelBuilder.Entity<User>().HasData(user);

        modelBuilder.Entity<Item>().HasData(
            new Item
            {
                ItemID = 1,
                UserId = user.UserID,
                ItemName = "GuLongBraised Peanuts 170g",
                ItemType = "Canned Goods",
                Quantity = 1,
                DatePurchased = new DateTime(2023, 7, 26, 0, 0, 0), // July 26, 2023, 12:00:00 am
                DateExpiresOn = new DateTime(2025, 12, 12, 0, 0, 0), // December 12, 2025, 12:00:00 am
                ImagePath = "assets/images/canned-goods.png",
                StoredAt = "Pantry",
                Description = "This is a can of braised peanuts"
            },
            new Item
            {
                ItemID = 2,
                UserId = user.UserID,
                ItemName = "โก๋แก่ Koh-Kae Peanuts Nori Wasabi Flavour Coated Delicious With Japanese Seaweed 160g",
                ItemType = "Snack Foods",
                Quantity = 3,
                DatePurchased = new DateTime(2023, 7, 11, 0, 0, 0), // July 11, 2023, 12:00:00 am
                DateExpiresOn = new DateTime(2023, 11, 14, 0, 0, 0), // November 11, 2023, 12:00:00 am
                ImagePath = "assets/images/snack-foods.png",
                StoredAt = "Pantry",
                Description = "This is a can of braised peanuts"
            },
            new Item
            {
                ItemID = 3,
                UserId = user.UserID,
                ItemName = "Shiitake Mushrooms (Packed)",
                ItemType = "Fresh Produce",
                Quantity = 6,
                DatePurchased = new DateTime(2023, 7, 20, 0, 0, 0), // July 20, 2023, 12:00:00 am
                DateExpiresOn = new DateTime(2024, 1, 15, 0, 0, 0), // January 15, 2024, 12:00:00 am
                ImagePath = "assets/images/fresh-produce.png",
                StoredAt = "Fridge",
                Description = "Fresh milk from Dutch Lady brand"
            },
            new Item
            {
                ItemID = 4,
                UserId = user.UserID,
                ItemName = "Dutch Lady Milk",
                ItemType = "Refrigerated",
                Quantity = 3,
                DatePurchased = new DateTime(2023, 7, 15, 0, 0, 0), // July 15, 2023, 12:00:00 am
                DateExpiresOn = new DateTime(2023, 8, 15, 0, 0, 0), // August 15, 2023, 12:00:00 am
                ImagePath = "assets/images/refrigerated.png",
                StoredAt = "Fridge",
                Description = "Packed shiitake mushrooms for cooking"
            },
            new Item
            {
                ItemID = 5,
                UserId = user.UserID,
                ItemName = "Macaroni Pasta 500g",
                ItemType = "Non-perishables",
                Quantity = 4,
                DatePurchased = new DateTime(2023, 7, 10, 0, 0, 0), // July 10, 2023, 12:00:00 am
                DateExpiresOn = new DateTime(2024, 7, 10, 0, 0, 0), // July 10, 2024, 12:00:00 am
                ImagePath = "assets/images/non-perishables.png",
                StoredAt = "Cupboard",
                Description = "Macaroni pasta, great for cooking"
            }
            );

        modelBuilder.Entity<Item>()
            .HasOne<User>(i => i.User)
            .WithMany(u => u.Items)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
