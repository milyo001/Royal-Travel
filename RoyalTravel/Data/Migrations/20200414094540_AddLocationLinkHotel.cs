using Microsoft.EntityFrameworkCore.Migrations;

namespace RoyalTravel.Data.Migrations
{
    public partial class AddLocationLinkHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LocationLink",
                table: "Hotels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationLink",
                table: "Hotels");
        }
    }
}
