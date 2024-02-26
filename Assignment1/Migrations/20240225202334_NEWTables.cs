using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assignment1.Migrations
{
    /// <inheritdoc />
    public partial class NEWTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: " ArrivalCity",
                table: "Flights",
                newName: "ArrivalCity");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "CarRentals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PickupDate",
                table: "CarRentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ReturnDate",
                table: "CarRentals",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "PickupDate",
                table: "CarRentals");

            migrationBuilder.DropColumn(
                name: "ReturnDate",
                table: "CarRentals");

            migrationBuilder.RenameColumn(
                name: "ArrivalCity",
                table: "Flights",
                newName: " ArrivalCity");
        }
    }
}
