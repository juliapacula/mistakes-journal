using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddCreateAtDateToMistake : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "mistake",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "mistake");
        }
    }
}
