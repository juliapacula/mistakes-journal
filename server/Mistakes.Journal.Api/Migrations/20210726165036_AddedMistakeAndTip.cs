using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddedMistakeAndTip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mistake",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    goal = table.Column<string>(nullable: true),
                    priority = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mistake", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tip",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    content = table.Column<string>(nullable: false),
                    mistake_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tip", x => x.id);
                    table.ForeignKey(
                        name: "fk_tip_mistake_mistake_id1",
                        column: x => x.mistake_id,
                        principalTable: "mistake",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_tip_mistake_id",
                table: "tip",
                column: "mistake_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tip");

            migrationBuilder.DropTable(
                name: "mistake");
        }
    }
}
