using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddAdditionalQuestions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "can_i_fix_it",
                table: "mistake",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "consequences",
                table: "mistake",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "only_responsible",
                table: "mistake",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "what_can_i_do_better",
                table: "mistake",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "what_did_i_learn",
                table: "mistake",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "can_i_fix_it",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "consequences",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "only_responsible",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "what_can_i_do_better",
                table: "mistake");

            migrationBuilder.DropColumn(
                name: "what_did_i_learn",
                table: "mistake");
        }
    }
}
