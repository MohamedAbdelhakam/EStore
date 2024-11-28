using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddIsdeletedToProductAndCategory02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1aa12bc6-f272-4f79-98b0-20d6d600d23d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "856e11c1-29ff-45ee-961c-df8f80a33a97");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d47f37d5-6809-4023-8f94-33e437c2c147");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Cart",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "799cc280-ae1b-4343-9a95-2785a92845db", null, "Manager", "MANAGER" },
                    { "c9b2f5f3-b361-47c3-af7f-3ca2bf4c53fa", null, "Administrator", "ADMINISTRATOR" },
                    { "dfeaf86d-5384-424b-b009-647d18f02f55", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "799cc280-ae1b-4343-9a95-2785a92845db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9b2f5f3-b361-47c3-af7f-3ca2bf4c53fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfeaf86d-5384-424b-b009-647d18f02f55");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cart");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1aa12bc6-f272-4f79-98b0-20d6d600d23d", null, "Customer", "CUSTOMER" },
                    { "856e11c1-29ff-45ee-961c-df8f80a33a97", null, "Administrator", "ADMINISTRATOR" },
                    { "d47f37d5-6809-4023-8f94-33e437c2c147", null, "Manager", "MANAGER" }
                });
        }
    }
}
