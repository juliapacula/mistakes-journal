using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class TipDoesNotRequireMistake : Migration
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
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AlterColumn<Guid>(
                name: "mistake_id",
                table: "tip",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AlterColumn<Guid>(
                name: "mistake_id",
                table: "tip",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
