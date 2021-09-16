using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class RenamedRepetitionOccurredAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "occurred_at",
                table: "repetition",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.Sql($"UPDATE repetition SET occurred_at = occured_at;");
            
            migrationBuilder.DropColumn(
                name: "occured_at",
                table: "repetition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "occured_at",
                table: "repetition",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
            
            migrationBuilder.Sql($"UPDATE repetition SET occured_at = occurred_at;");

            migrationBuilder.DropColumn(
                name: "occurred_at",
                table: "repetition");
        }
    }
}
