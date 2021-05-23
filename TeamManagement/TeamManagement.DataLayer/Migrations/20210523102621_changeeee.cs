using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamManagement.DataLayer.Migrations
{
    public partial class changeeee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfPublishsing",
                table: "Reports",
                newName: "DateOfPublishing");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfPublishing",
                table: "Reports",
                newName: "DateOfPublishsing");
        }
    }
}
