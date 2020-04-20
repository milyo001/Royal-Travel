using Microsoft.EntityFrameworkCore.Migrations;

namespace RoyalTravel.Data.Migrations
{
    public partial class UsedPointsChangeToIntWasNullabeInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UsedPoints",
                table: "Users",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UsedPoints",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
