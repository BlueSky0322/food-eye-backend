using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodEyeAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    DatePurchased = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    StoredAt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserRole = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemID", "DateExpiresOn", "DatePurchased", "Description", "ImagePath", "ItemName", "ItemType", "Quantity", "StoredAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a can of braised peanuts", "assets/images/canned-goods.png", "GuLongBraised Peanuts 170g", "Canned Goods", 1, "Pantry" },
                    { 2, new DateTime(2023, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "This is a can of braised peanuts", "assets/images/snack-foods.png", "โก๋แก่ Koh-Kae Peanuts Nori Wasabi Flavour Coated Delicious With Japanese Seaweed 160g", "Snack Foods", 3, "Pantry" },
                    { 3, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fresh milk from Dutch Lady brand", "assets/images/fresh-produce.png", "Shiitake Mushrooms (Packed)", "Fresh Produce", 6, "Fridge" },
                    { 4, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Packed shiitake mushrooms for cooking", "assets/images/refrigerated.png", "Dutch Lady Milk", "Refrigerated", 3, "Fridge" },
                    { 5, new DateTime(2024, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Macaroni pasta, great for cooking", "assets/images/non-perishables.png", "Macaroni Pasta 500g", "Non-perishables", 4, "Cupboard" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Age", "DateOfBirth", "Email", "Name", "Password", "UserRole" },
                values: new object[] { "411d825b-b796-44b2-ae5f-d0e4d8c31dcf", "123 Main Street", 30, new DateTime(1993, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@a.a", "John Doe", "123123", "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
