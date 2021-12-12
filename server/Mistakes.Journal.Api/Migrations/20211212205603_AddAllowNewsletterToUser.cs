using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddAllowNewsletterToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "agree_to_newsletter",
                table: "user",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "agree_to_newsletter",
                table: "user");
        }
    }
}
