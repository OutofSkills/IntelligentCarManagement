using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class RideModelRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Destination",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PickUpLocation",
                table: "Rides");

            migrationBuilder.RenameColumn(
                name: "DestinationCoordinates",
                table: "Rides",
                newName: "PickUpPlaceName");

            migrationBuilder.AddColumn<double>(
                name: "AverageTime",
                table: "Rides",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "DestinationPlaceAddress",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationPlaceLat",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationPlaceLong",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DestinationPlaceName",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Distance",
                table: "Rides",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "PickUpPlaceAddress",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpPlaceLat",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PickUpPlaceLong",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AverageTime",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DestinationPlaceAddress",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DestinationPlaceLat",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DestinationPlaceLong",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "DestinationPlaceName",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PickUpPlaceAddress",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PickUpPlaceLat",
                table: "Rides");

            migrationBuilder.DropColumn(
                name: "PickUpPlaceLong",
                table: "Rides");

            migrationBuilder.RenameColumn(
                name: "PickUpPlaceName",
                table: "Rides",
                newName: "DestinationCoordinates");

            migrationBuilder.AddColumn<string>(
                name: "Destination",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PickUpLocation",
                table: "Rides",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
