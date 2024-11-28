using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
