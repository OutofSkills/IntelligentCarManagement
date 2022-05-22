using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class ApplicationStatusRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverApplications_ApplicationStatuses_StatusId",
                table: "DriverApplications");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "DriverApplications",
                newName: "ApplicationStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverApplications_StatusId",
                table: "DriverApplications",
                newName: "IX_DriverApplications_ApplicationStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverApplications_ApplicationStatuses_ApplicationStatusId",
                table: "DriverApplications",
                column: "ApplicationStatusId",
                principalTable: "ApplicationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DriverApplications_ApplicationStatuses_ApplicationStatusId",
                table: "DriverApplications");

            migrationBuilder.RenameColumn(
                name: "ApplicationStatusId",
                table: "DriverApplications",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_DriverApplications_ApplicationStatusId",
                table: "DriverApplications",
                newName: "IX_DriverApplications_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_DriverApplications_ApplicationStatuses_StatusId",
                table: "DriverApplications",
                column: "StatusId",
                principalTable: "ApplicationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
