using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodEyeAPI.Migrations
{
    /// <inheritdoc />
    public partial class AssociateUserToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "411d825b-b796-44b2-ae5f-d0e4d8c31dcf");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Items",
                type: "nvarchar(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 1,
                column: "UserId",
                value: "c62b40ba-27ac-4d0b-9bf9-a6361540da00");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 2,
                column: "UserId",
                value: "c62b40ba-27ac-4d0b-9bf9-a6361540da00");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 3,
                column: "UserId",
                value: "c62b40ba-27ac-4d0b-9bf9-a6361540da00");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 4,
                column: "UserId",
                value: "c62b40ba-27ac-4d0b-9bf9-a6361540da00");

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemID",
                keyValue: 5,
                column: "UserId",
                value: "c62b40ba-27ac-4d0b-9bf9-a6361540da00");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Age", "DateOfBirth", "Email", "Name", "Password", "UserRole" },
                values: new object[] { "c62b40ba-27ac-4d0b-9bf9-a6361540da00", "123 Main Street", 30, new DateTime(1993, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@a.a", "John Doe", "123123", "Admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_UserId",
                table: "Items",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Users_UserId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_UserId",
                table: "Items");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: "c62b40ba-27ac-4d0b-9bf9-a6361540da00");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Items");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Address", "Age", "DateOfBirth", "Email", "Name", "Password", "UserRole" },
                values: new object[] { "411d825b-b796-44b2-ae5f-d0e4d8c31dcf", "123 Main Street", 30, new DateTime(1993, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@a.a", "John Doe", "123123", "Admin" });
        }
    }
}
