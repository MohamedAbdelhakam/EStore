using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EStore.Core.Migrations
{
    /// <inheritdoc />
    public partial class DropProductPricefromOrderTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldComputedColumnSql: "[ProductPrice]+[ShippingPrice]");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "Orders");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProductPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18,2)",
                nullable: false,
                computedColumnSql: "[ProductPrice]+[ShippingPrice]",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
