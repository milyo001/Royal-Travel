using Microsoft.EntityFrameworkCore.Migrations;

namespace RoyalTravel.Data.Migrations
{
    public partial class AddConfNumberInStays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stays_User_ApplicationUserId",
                table: "Stays");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Stays",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationNumber",
                table: "Stays",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Stays_User_ApplicationUserId",
                table: "Stays",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stays_User_ApplicationUserId",
                table: "Stays");

            migrationBuilder.DropColumn(
                name: "ConfirmationNumber",
                table: "Stays");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Stays",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Stays_User_ApplicationUserId",
                table: "Stays",
                column: "ApplicationUserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
