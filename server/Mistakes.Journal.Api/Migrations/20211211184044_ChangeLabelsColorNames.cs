using Microsoft.EntityFrameworkCore.Migrations;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class ChangeLabelsColorNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update label set color='#FFC145' where color='#F6CA2C';");
            migrationBuilder.Sql(@"update label set color='#A66FFF' where color='#946FFF';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"update label set color='#F6CA2C' where color='#FFC145';");
            migrationBuilder.Sql(@"update label set color='#946FFF' where color='#A66FFF';");
        }
    }
}
