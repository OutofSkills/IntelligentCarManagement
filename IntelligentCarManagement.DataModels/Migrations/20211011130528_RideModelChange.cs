using Microsoft.EntityFrameworkCore.Migrations;

namespace IntelligentCarManagement.Api.DataAccess.Migrations
{
    public partial class RideModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Rides",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_ClientId",
                table: "Rides",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_AspNetUsers_ClientId",
                table: "Rides",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_AspNetUsers_ClientId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_ClientId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Rides");
        }
    }
}
