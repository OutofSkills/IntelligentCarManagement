using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class NullableRideRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Reviews_RideReviewId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_RideReviewId",
                table: "Rides");

            migrationBuilder.AlterColumn<int>(
                name: "RideReviewId",
                table: "Rides",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Rides_RideReviewId",
                table: "Rides",
                column: "RideReviewId",
                unique: true,
                filter: "[RideReviewId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Reviews_RideReviewId",
                table: "Rides",
                column: "RideReviewId",
                principalTable: "Reviews",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rides_Reviews_RideReviewId",
                table: "Rides");

            migrationBuilder.DropIndex(
                name: "IX_Rides_RideReviewId",
                table: "Rides");

            migrationBuilder.AlterColumn<int>(
                name: "RideReviewId",
                table: "Rides",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rides_RideReviewId",
                table: "Rides",
                column: "RideReviewId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rides_Reviews_RideReviewId",
                table: "Rides",
                column: "RideReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
