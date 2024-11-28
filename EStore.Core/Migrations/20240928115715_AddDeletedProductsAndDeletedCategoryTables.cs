using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddDeletedProductsAndDeletedCategoryTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e6358c7-d0fe-4c5f-b6ea-943b7462e853");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bcd12ea4-ae16-47bb-96bf-c903c793952d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c3ee0416-09ae-4511-89c8-437443250564");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Cart");

            migrationBuilder.AddColumn<int>(
                name: "DeletedCategoryId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedProductId",
                table: "OrderProduct",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DeletedCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeletedProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitsInStock = table.Column<int>(type: "int", nullable: false),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagesUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeletedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeletedProducts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_DeletedProductId",
                table: "OrderProduct",
                column: "DeletedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_DeletedProducts_CategoryId",
                table: "DeletedProducts",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_DeletedProducts_DeletedProductId",
                table: "OrderProduct",
                column: "DeletedProductId",
                principalTable: "DeletedProducts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_DeletedCategories_DeletedCategoryId",
                table: "Products",
                column: "DeletedCategoryId",
                principalTable: "DeletedCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_DeletedProducts_DeletedProductId",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_DeletedCategories_DeletedCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "DeletedCategories");

            migrationBuilder.DropTable(
                name: "DeletedProducts");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeletedCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_DeletedProductId",
                table: "OrderProduct");

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

            migrationBuilder.DropColumn(
                name: "DeletedProductId",
                table: "OrderProduct");

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
                    { "0e6358c7-d0fe-4c5f-b6ea-943b7462e853", null, "Customer", "CUSTOMER" },
                    { "bcd12ea4-ae16-47bb-96bf-c903c793952d", null, "Manager", "MANAGER" },
                    { "c3ee0416-09ae-4511-89c8-437443250564", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
