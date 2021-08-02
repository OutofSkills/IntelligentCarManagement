using Microsoft.EntityFrameworkCore.Migrations;

namespace IntelligentCarManagement.DataAccess.Migrations
{
    public partial class DriverChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Accidents",
                table: "Driver",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accidents",
                table: "Driver");
        }
    }
}
