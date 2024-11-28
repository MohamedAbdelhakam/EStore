using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class MakeRefreshTokenNullabkeInTableUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e9c11f6-068d-4fb2-b6fc-eeb76f7f7344");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "611fe6bb-ed4f-4c61-b7aa-f4634aa38122");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca10880-bc76-4324-80d3-3b9b6ffd92bf");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3365c33f-50b6-42bc-8b06-807e2fac54d3", null, "Administrator", "ADMINISTRATOR" },
                    { "38889e85-7cee-4adb-ab48-1f36ce0f0026", null, "Manager", "MANAGER" },
                    { "82528e5d-e621-439c-b32d-b21fed7ab3a7", null, "Customer", "CUSTOMER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3365c33f-50b6-42bc-8b06-807e2fac54d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "38889e85-7cee-4adb-ab48-1f36ce0f0026");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "82528e5d-e621-439c-b32d-b21fed7ab3a7");

            migrationBuilder.AlterColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e9c11f6-068d-4fb2-b6fc-eeb76f7f7344", null, "Administrator", "ADMINISTRATOR" },
                    { "611fe6bb-ed4f-4c61-b7aa-f4634aa38122", null, "Customer", "CUSTOMER" },
                    { "7ca10880-bc76-4324-80d3-3b9b6ffd92bf", null, "Manager", "MANAGER" }
                });
        }
    }
}
