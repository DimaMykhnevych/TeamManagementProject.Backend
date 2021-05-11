using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamManagement.DataLayer.Migrations
{
    public partial class AddReportsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Resolved = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeReview = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfPublishsing = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PublisherId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PublisherId",
                table: "Reports",
                column: "PublisherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
