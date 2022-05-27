using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class NotifcationCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EventName",
                table: "Notifications",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "EventContent",
                table: "Notifications",
                newName: "Body");

            migrationBuilder.AddColumn<int>(
                name: "NotificationCategoryId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "NotificaionCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificaionCategory", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_NotificationCategoryId",
                table: "Notifications",
                column: "NotificationCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_NotificaionCategory_NotificationCategoryId",
                table: "Notifications",
                column: "NotificationCategoryId",
                principalTable: "NotificaionCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_NotificaionCategory_NotificationCategoryId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "NotificaionCategory");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_NotificationCategoryId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "NotificationCategoryId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Notifications",
                newName: "EventName");

            migrationBuilder.RenameColumn(
                name: "Body",
                table: "Notifications",
                newName: "EventContent");
        }
    }
}
