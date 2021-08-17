using Microsoft.EntityFrameworkCore.Migrations;

namespace IntelligentCarManagement.Api.DataAccess.Migrations
{
    public partial class CarDrvierOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Driver_DriverID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_DriverID",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "DriverID",
                table: "Car",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Car_DriverID",
                table: "Car",
                column: "DriverID",
                unique: true,
                filter: "[DriverID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Driver_DriverID",
                table: "Car",
                column: "DriverID",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Driver_DriverID",
                table: "Car");

            migrationBuilder.DropIndex(
                name: "IX_Car_DriverID",
                table: "Car");

            migrationBuilder.AlterColumn<int>(
                name: "DriverID",
                table: "Car",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_DriverID",
                table: "Car",
                column: "DriverID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Driver_DriverID",
                table: "Car",
                column: "DriverID",
                principalTable: "Driver",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
