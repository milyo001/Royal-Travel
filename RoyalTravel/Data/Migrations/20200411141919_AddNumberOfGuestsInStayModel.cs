using Microsoft.EntityFrameworkCore.Migrations;

namespace RoyalTravel.Data.Migrations
{
    public partial class AddNumberOfGuestsInStayModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Adults",
                table: "Stays",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Children",
                table: "Stays",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adults",
                table: "Stays");

            migrationBuilder.DropColumn(
                name: "Children",
                table: "Stays");
        }
    }
}
