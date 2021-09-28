using Microsoft.EntityFrameworkCore.Migrations;

namespace IntelligentCarManagement.Api.DataAccess.Migrations
{
    public partial class DriverState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Driver",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Driver");
        }
    }
}
