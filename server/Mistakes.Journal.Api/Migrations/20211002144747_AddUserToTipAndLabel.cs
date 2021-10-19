using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Mistakes.Journal.Api.Migrations
{
    public partial class AddUserToTipAndLabel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "tip",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "label",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_tip_user_id",
                table: "tip",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_label_user_id",
                table: "label",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_label_users_user_id",
                table: "label",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_tip_users_user_id",
                table: "tip",
                column: "user_id",
                principalTable: "user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_label_users_user_id",
                table: "label");

            migrationBuilder.DropForeignKey(
                name: "fk_tip_users_user_id",
                table: "tip");

            migrationBuilder.DropIndex(
                name: "ix_tip_user_id",
                table: "tip");

            migrationBuilder.DropIndex(
                name: "ix_label_user_id",
                table: "label");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tip");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "label");
        }
    }
}
