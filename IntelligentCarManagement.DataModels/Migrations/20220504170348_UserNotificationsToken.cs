using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class UserNotificationsToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationsToken",
                table: "Clients");

            migrationBuilder.AddColumn<string>(
                name: "NotificationsToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationsToken",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "NotificationsToken",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
