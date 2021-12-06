using Microsoft.EntityFrameworkCore.Migrations;

namespace Vega.Web.Migrations
{
    public partial class vehicle_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisteredPersonName",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "PersonEmail",
                table: "Vehicles",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonName",
                table: "Vehicles",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonPhone",
                table: "Vehicles",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonEmail",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PersonName",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "PersonPhone",
                table: "Vehicles");

            migrationBuilder.AddColumn<string>(
                name: "RegisteredPersonName",
                table: "Vehicles",
                type: "varchar(255) CHARACTER SET utf8mb4",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
