using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class ApplicationStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverApplications_DriverStatuses_StatusId",
                table: "DriverApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DriverStatuses",
                table: "DriverStatuses");

            migrationBuilder.RenameTable(
                name: "DriverStatuses",
                newName: "ApplicationStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationStatuses",
                table: "ApplicationStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverApplications_ApplicationStatuses_StatusId",
                table: "DriverApplications",
                column: "StatusId",
                principalTable: "ApplicationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverApplications_ApplicationStatuses_StatusId",
                table: "DriverApplications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationStatuses",
                table: "ApplicationStatuses");

            migrationBuilder.RenameTable(
                name: "ApplicationStatuses",
                newName: "DriverStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DriverStatuses",
                table: "DriverStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverApplications_DriverStatuses_StatusId",
                table: "DriverApplications",
                column: "StatusId",
                principalTable: "DriverStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
