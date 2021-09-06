using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class ChangeDateTimeToOccuredAtInRepetition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date_time",
                table: "repetition",
                newName: "occured_at");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "occured_at",
                table: "repetition",
                newName: "date_time");
        }
    }
}
