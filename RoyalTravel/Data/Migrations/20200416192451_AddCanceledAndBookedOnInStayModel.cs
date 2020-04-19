using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RoyalTravel.Data.Migrations
{
    public partial class AddCanceledAndBookedOnInStayModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BookedOn",
                table: "Stays",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Stays",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookedOn",
                table: "Stays");

            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Stays");
        }
    }
}
