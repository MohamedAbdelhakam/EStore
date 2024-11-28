using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class dropcolumnDeletedCategoryIdFromProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_DeletedCategories_DeletedCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeletedCategoryId",
                table: "Products");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08f9bd62-dbb2-4808-a634-056133213150");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a1c707b3-815b-4d2a-87e6-2b8923530fe6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9a6f433-25f7-4c06-8593-ce9f84eb1421");

            migrationBuilder.DropColumn(
                name: "DeletedCategoryId",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4d9426c7-bd30-43fb-995b-df8ec920c6e0", null, "Customer", "CUSTOMER" },
                    { "896e43fc-3db8-4e03-97de-1bc9d23a897f", null, "Manager", "MANAGER" },
                    { "e9fe5156-18fc-4ced-8953-9f37713d2a72", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d9426c7-bd30-43fb-995b-df8ec920c6e0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "896e43fc-3db8-4e03-97de-1bc9d23a897f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9fe5156-18fc-4ced-8953-9f37713d2a72");

            migrationBuilder.AddColumn<int>(
                name: "DeletedCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08f9bd62-dbb2-4808-a634-056133213150", null, "Administrator", "ADMINISTRATOR" },
                    { "a1c707b3-815b-4d2a-87e6-2b8923530fe6", null, "Manager", "MANAGER" },
                    { "b9a6f433-25f7-4c06-8593-ce9f84eb1421", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeletedCategoryId",
                table: "Products",
                column: "DeletedCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DeletedCategories_DeletedCategoryId",
                table: "Products",
                column: "DeletedCategoryId",
                principalTable: "DeletedCategories",
                principalColumn: "Id");
        }
    }
}
