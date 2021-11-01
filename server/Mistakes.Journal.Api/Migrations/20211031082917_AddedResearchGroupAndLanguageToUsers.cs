using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddedResearchGroupAndLanguageToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "group",
                table: "user",
                nullable: false,
                defaultValue: "Default");

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "user",
                nullable: false,
                defaultValue: "EN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "group",
                table: "user");

            migrationBuilder.DropColumn(
                name: "language",
                table: "user");
        }
    }
}
