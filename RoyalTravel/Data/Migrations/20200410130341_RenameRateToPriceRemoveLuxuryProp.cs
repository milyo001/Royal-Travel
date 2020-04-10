using Microsoft.EntityFrameworkCore.Migrations;

namespace RoyalTravel.Data.Migrations
{
    public partial class RenameRateToPriceRemoveLuxuryProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Stays");

            migrationBuilder.DropColumn(
                name: "Available",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Luxury",
                table: "Rooms");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Stays",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Stays");

            migrationBuilder.AddColumn<double>(
                name: "Rate",
                table: "Stays",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "Available",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Luxury",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
