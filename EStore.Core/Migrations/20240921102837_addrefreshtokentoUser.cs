using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class addrefreshtokentoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21031d62-45e7-4c9a-b9b1-e1ffc85d1a99");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56e4fdf5-b8a3-4eb1-8968-5280ddac2cfe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75a4a0ef-cfd8-44f2-8ca0-2eff5a52a6e4");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2dd66ecd-97a7-4305-8a88-df2c10470048", null, "Manager", "MANAGER" },
                    { "3c6b21fb-e2b3-4cf1-a8e8-7cc8748b8911", null, "Administrator", "ADMINISTRATOR" },
                    { "58bf320e-7624-40c5-9502-ea59164470b3", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dd66ecd-97a7-4305-8a88-df2c10470048");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c6b21fb-e2b3-4cf1-a8e8-7cc8748b8911");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "58bf320e-7624-40c5-9502-ea59164470b3");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21031d62-45e7-4c9a-b9b1-e1ffc85d1a99", null, "Administrator", "ADMINISTRATOR" },
                    { "56e4fdf5-b8a3-4eb1-8968-5280ddac2cfe", null, "Customer", "CUSTOMER" },
                    { "75a4a0ef-cfd8-44f2-8ca0-2eff5a52a6e4", null, "Manager", "MANAGER" }
                });
        }
    }
}
