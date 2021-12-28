using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddRegistrationDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "registered_at",
                table: "user",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql(@"UPDATE ""user"" SET registered_at = last_logging_in WHERE last_logging_in IS NOT NULL;");
            migrationBuilder.Sql(@"UPDATE ""user"" SET registered_at = now() WHERE last_logging_in IS NULL;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "registered_at",
                table: "user");
        }
    }
}
