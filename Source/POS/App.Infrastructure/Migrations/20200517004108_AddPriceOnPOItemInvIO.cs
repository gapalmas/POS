using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.Migrations
{
    public partial class AddPriceOnPOItemInvIO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Orderitemssales",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Inventoryio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Orderitemssales");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Inventoryio");
        }
    }
}
