using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class RemoveFirstLastNameAddAgeCountryToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "user");

            migrationBuilder.AddColumn<int>(
                name: "age",
                table: "user",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "user",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"DELETE FROM ""user"";");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "age",
                table: "user");

            migrationBuilder.DropColumn(
                name: "country",
                table: "user");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
