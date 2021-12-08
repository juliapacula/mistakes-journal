using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddLastLoggingInAndCounterToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "last_logging_in",
                table: "user",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "logged_days_count",
                table: "user",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_logging_in",
                table: "user");

            migrationBuilder.DropColumn(
                name: "logged_days_count",
                table: "user");
        }
    }
}
