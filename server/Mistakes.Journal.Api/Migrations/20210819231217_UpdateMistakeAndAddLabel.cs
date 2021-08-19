using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class UpdateMistakeAndAddLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "counter",
                table: "mistake",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "first_occurence_date_time",
                table: "mistake",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "last_progress_in_days",
                table: "mistake",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_repetition_date_time",
                table: "mistake",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_repetition_registration_date_time",
                table: "mistake",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "label",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    color = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_label", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mistake_label",
                columns: table => new
                {
                    mistake_id = table.Column<Guid>(nullable: false),
                    label_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mistake_label", x => new { x.mistake_id, x.label_id });
                    table.ForeignKey(
                        name: "fk_mistake_label_label_label_id",
                        column: x => x.label_id,
                        principalTable: "label",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mistake_label_mistake_mistake_id",
                        column: x => x.mistake_id,
                        principalTable: "mistake",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_mistake_label_label_id",
                table: "mistake_label",
                column: "label_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mistake_label");

            migrationBuilder.DropTable(
                name: "label");

            migrationBuilder.DropColumn(
                name: "counter",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "first_occurence_date_time",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "last_progress_in_days",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "last_repetition_date_time",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "last_repetition_registration_date_time",
                table: "mistake");
        }
    }
}
