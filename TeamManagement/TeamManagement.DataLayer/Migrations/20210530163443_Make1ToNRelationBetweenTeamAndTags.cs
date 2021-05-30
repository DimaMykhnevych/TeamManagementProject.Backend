using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamManagement.DataLayer.Migrations
{
    public partial class Make1ToNRelationBetweenTeamAndTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Tags",
                type: "uniqueidentifier",
                nullable: true,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TeamId",
                table: "Tags",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Teams_TeamId",
                table: "Tags",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Teams_TeamId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_TeamId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Tags");
        }
    }
}
