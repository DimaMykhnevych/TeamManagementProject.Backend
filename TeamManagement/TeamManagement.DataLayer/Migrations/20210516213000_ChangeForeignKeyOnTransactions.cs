using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamManagement.DataLayer.Migrations
{
    public partial class ChangeForeignKeyOnTransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubscriptionPlans_Subscriptions_SubscriptionId",
                table: "SubscriptionPlans");

            migrationBuilder.DropIndex(
                name: "IX_SubscriptionPlans_SubscriptionId",
                table: "SubscriptionPlans");

            migrationBuilder.DropColumn(
                name: "SubscriptionId",
                table: "SubscriptionPlans");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionPlanId",
                table: "Subscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubscriptionPlanId",
                table: "Subscriptions",
                column: "SubscriptionPlanId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubscriptionPlans_SubscriptionPlanId",
                table: "Subscriptions",
                column: "SubscriptionPlanId",
                principalTable: "SubscriptionPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubscriptionPlans_SubscriptionPlanId",
                table: "Subscriptions");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubscriptionPlanId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubscriptionPlanId",
                table: "Subscriptions");

            migrationBuilder.AddColumn<Guid>(
                name: "SubscriptionId",
                table: "SubscriptionPlans",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlans_SubscriptionId",
                table: "SubscriptionPlans",
                column: "SubscriptionId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubscriptionPlans_Subscriptions_SubscriptionId",
                table: "SubscriptionPlans",
                column: "SubscriptionId",
                principalTable: "Subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
