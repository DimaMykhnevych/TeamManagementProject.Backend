using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamManagement.DataLayer.Migrations
{
    public partial class DeleteTransactionKeyProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionKey",
                table: "Subscriptions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionKey",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
