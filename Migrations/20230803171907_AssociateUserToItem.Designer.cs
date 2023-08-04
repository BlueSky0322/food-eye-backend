﻿// <auto-generated />
using System;
using FoodEyeAPI.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodEyeAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230803171907_AssociateUserToItem")]
    partial class AssociateUserToItem
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodEyeAPI.Models.Table.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"));

                    b.Property<DateTime>("DateExpiresOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatePurchased")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ImagePath")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ItemName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ItemType")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("StoredAt")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(36)");

                    b.HasKey("ItemID");

                    b.HasIndex("UserId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            ItemID = 1,
                            DateExpiresOn = new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatePurchased = new DateTime(2023, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "This is a can of braised peanuts",
                            ImagePath = "assets/images/canned-goods.png",
                            ItemName = "GuLongBraised Peanuts 170g",
                            ItemType = "Canned Goods",
                            Quantity = 1,
                            StoredAt = "Pantry",
                            UserId = "c62b40ba-27ac-4d0b-9bf9-a6361540da00"
                        },
                        new
                        {
                            ItemID = 2,
                            DateExpiresOn = new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatePurchased = new DateTime(2023, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "This is a can of braised peanuts",
                            ImagePath = "assets/images/snack-foods.png",
                            ItemName = "โก๋แก่ Koh-Kae Peanuts Nori Wasabi Flavour Coated Delicious With Japanese Seaweed 160g",
                            ItemType = "Snack Foods",
                            Quantity = 3,
                            StoredAt = "Pantry",
                            UserId = "c62b40ba-27ac-4d0b-9bf9-a6361540da00"
                        },
                        new
                        {
                            ItemID = 3,
                            DateExpiresOn = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatePurchased = new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Fresh milk from Dutch Lady brand",
                            ImagePath = "assets/images/fresh-produce.png",
                            ItemName = "Shiitake Mushrooms (Packed)",
                            ItemType = "Fresh Produce",
                            Quantity = 6,
                            StoredAt = "Fridge",
                            UserId = "c62b40ba-27ac-4d0b-9bf9-a6361540da00"
                        },
                        new
                        {
                            ItemID = 4,
                            DateExpiresOn = new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatePurchased = new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Packed shiitake mushrooms for cooking",
                            ImagePath = "assets/images/refrigerated.png",
                            ItemName = "Dutch Lady Milk",
                            ItemType = "Refrigerated",
                            Quantity = 3,
                            StoredAt = "Fridge",
                            UserId = "c62b40ba-27ac-4d0b-9bf9-a6361540da00"
                        },
                        new
                        {
                            ItemID = 5,
                            DateExpiresOn = new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            DatePurchased = new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Macaroni pasta, great for cooking",
                            ImagePath = "assets/images/non-perishables.png",
                            ItemName = "Macaroni Pasta 500g",
                            ItemType = "Non-perishables",
                            Quantity = 4,
                            StoredAt = "Cupboard",
                            UserId = "c62b40ba-27ac-4d0b-9bf9-a6361540da00"
                        });
                });

            modelBuilder.Entity("FoodEyeAPI.Models.Table.User", b =>
                {
                    b.Property<string>("UserID")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("UserID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = "c62b40ba-27ac-4d0b-9bf9-a6361540da00",
                            Address = "123 Main Street",
                            Age = 30,
                            DateOfBirth = new DateTime(1993, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "a@a.a",
                            Name = "John Doe",
                            Password = "123123",
                            UserRole = "Admin"
                        });
                });

            modelBuilder.Entity("FoodEyeAPI.Models.Table.Item", b =>
                {
                    b.HasOne("FoodEyeAPI.Models.Table.User", "User")
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodEyeAPI.Models.Table.User", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
