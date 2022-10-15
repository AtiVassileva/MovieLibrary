using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieLibrary.Data.Migrations
{
    public partial class AddedMoreUserProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentlyLoggedIn",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegistered",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentlyLoggedIn",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsRegistered",
                table: "Users");
        }
    }
}
