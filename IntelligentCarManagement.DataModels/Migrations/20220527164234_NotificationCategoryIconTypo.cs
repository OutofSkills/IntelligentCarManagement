using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class NotificationCategoryIconTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationCategory_NotificationCategoryId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationCategory",
                table: "NotificationCategory");

            migrationBuilder.RenameTable(
                name: "NotificationCategory",
                newName: "NotificationCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationCategories",
                table: "NotificationCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationCategories_NotificationCategoryId",
                table: "Notifications",
                column: "NotificationCategoryId",
                principalTable: "NotificationCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificationCategories_NotificationCategoryId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotificationCategories",
                table: "NotificationCategories");

            migrationBuilder.RenameTable(
                name: "NotificationCategories",
                newName: "NotificationCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotificationCategory",
                table: "NotificationCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificationCategory_NotificationCategoryId",
                table: "Notifications",
                column: "NotificationCategoryId",
                principalTable: "NotificationCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
