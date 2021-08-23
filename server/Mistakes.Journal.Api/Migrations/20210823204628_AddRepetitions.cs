using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddRepetitions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "color",
                table: "label",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "repetition",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    date_time = table.Column<DateTime>(nullable: false),
                    mistake_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_repetition", x => x.id);
                    table.ForeignKey(
                        name: "fk_repetition_mistake_mistake_id1",
                        column: x => x.mistake_id,
                        principalTable: "mistake",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_repetition_mistake_id",
                table: "repetition",
                column: "mistake_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "repetition");

            migrationBuilder.AddColumn<int>(
                name: "counter",
                table: "mistake",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "first_occurence_date_time",
                table: "mistake",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "last_progress_in_days",
                table: "mistake",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_repetition_date_time",
                table: "mistake",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "last_repetition_registration_date_time",
                table: "mistake",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "color",
                table: "label",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
