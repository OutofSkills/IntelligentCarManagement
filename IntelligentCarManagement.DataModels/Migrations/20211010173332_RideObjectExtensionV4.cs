using Microsoft.EntityFrameworkCore.Migrations;

namespace IntelligentCarManagement.Api.DataAccess.Migrations
{
    public partial class RideObjectExtensionV4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coordinates",
                table: "Rides",
                newName: "PickUpCoordinates");

            migrationBuilder.AddColumn<string>(
                name: "DestinationCoordinates",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationCoordinates",
                table: "Rides");

            migrationBuilder.RenameColumn(
                name: "PickUpCoordinates",
                table: "Rides",
                newName: "Coordinates");
        }
    }
}
