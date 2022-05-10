using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class RideModelFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_AspNetUsers_ClientId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Drivers_DriverId",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PickUpCoordinates",
                table: "Rides");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_AspNetUsers_ClientId",
                table: "Rides",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Drivers_DriverId",
                table: "Rides",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_AspNetUsers_ClientId",
                table: "Rides");

            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Drivers_DriverId",
                table: "Rides");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Rides",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Rides",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "PickUpCoordinates",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_AspNetUsers_ClientId",
                table: "Rides",
                column: "ClientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Drivers_DriverId",
                table: "Rides",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id");
        }
    }
}
