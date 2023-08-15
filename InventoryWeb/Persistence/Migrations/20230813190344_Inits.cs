using Microsoft.EntityFrameworkCore.Migrations;

namespace InventoryWeb.Data.Migrations
{
    public partial class Inits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ProductValue",
                table: "Products",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Value",
                table: "Inventories",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductValue",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "Inventories");
        }
    }
}
