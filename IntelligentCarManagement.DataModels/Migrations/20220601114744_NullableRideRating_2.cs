using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.DataAccess.Migrations
{
    public partial class NullableRideRating_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientReview_Clients_ClientId",
                table: "ClientReview");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientReview_Drivers_DriverId",
                table: "ClientReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientReview",
                table: "ClientReview");

            migrationBuilder.RenameTable(
                name: "ClientReview",
                newName: "ClientReviews");

            migrationBuilder.RenameIndex(
                name: "IX_ClientReview_DriverId",
                table: "ClientReviews",
                newName: "IX_ClientReviews_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientReview_ClientId",
                table: "ClientReviews",
                newName: "IX_ClientReviews_ClientId");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Reviews",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientReviews",
                table: "ClientReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReviews_Clients_ClientId",
                table: "ClientReviews",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReviews_Drivers_DriverId",
                table: "ClientReviews",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClientReviews_Clients_ClientId",
                table: "ClientReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientReviews_Drivers_DriverId",
                table: "ClientReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClientReviews",
                table: "ClientReviews");

            migrationBuilder.RenameTable(
                name: "ClientReviews",
                newName: "ClientReview");

            migrationBuilder.RenameIndex(
                name: "IX_ClientReviews_DriverId",
                table: "ClientReview",
                newName: "IX_ClientReview_DriverId");

            migrationBuilder.RenameIndex(
                name: "IX_ClientReviews_ClientId",
                table: "ClientReview",
                newName: "IX_ClientReview_ClientId");

            migrationBuilder.AlterColumn<double>(
                name: "Rating",
                table: "Reviews",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClientReview",
                table: "ClientReview",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReview_Clients_ClientId",
                table: "ClientReview",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ClientReview_Drivers_DriverId",
                table: "ClientReview",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
