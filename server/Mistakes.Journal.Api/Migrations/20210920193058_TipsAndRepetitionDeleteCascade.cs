using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class TipsAndRepetitionDeleteCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tip_mistake_mistake_id1",
                table: "tip");

            migrationBuilder.AddForeignKey(
                name: "fk_tip_mistake_mistake_id1",
                table: "tip",
                column: "mistake_id",
                principalTable: "mistake",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_tip_mistake_mistake_id1",
                table: "tip");

            migrationBuilder.AddForeignKey(
                name: "fk_tip_mistake_mistake_id1",
                table: "tip",
                column: "mistake_id",
                principalTable: "mistake",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
