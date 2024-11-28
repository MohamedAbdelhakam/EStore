using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminIdentifiertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "AdminIdentifiers",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId_Admin = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Taken = table.Column<bool>(type: "bit", nullable: false),
                    ManagerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminIdentifiers", x => x.AdminId);
                    table.ForeignKey(
                        name: "FK_AdminIdentifiers_AspNetUsers_AdminId",
                        column: x => x.AdminId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdminIdentifiers_AspNetUsers_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0e9c11f6-068d-4fb2-b6fc-eeb76f7f7344", null, "Administrator", "ADMINISTRATOR" },
                    { "611fe6bb-ed4f-4c61-b7aa-f4634aa38122", null, "Customer", "CUSTOMER" },
                    { "7ca10880-bc76-4324-80d3-3b9b6ffd92bf", null, "Manager", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminIdentifiers_ManagerId",
                table: "AdminIdentifiers",
                column: "ManagerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminIdentifiers");

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
    }
}
