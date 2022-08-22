using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSProject1.Migrations
{
    public partial class RemovedNavPropFromBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Booking_BookingFlightId_BookingPassengerId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Booking_BookingFlightId_BookingPassengerId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Passengers_BookingFlightId_BookingPassengerId",
                table: "Passengers");

            migrationBuilder.DropIndex(
                name: "IX_Flights_BookingFlightId_BookingPassengerId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "BookingFlightId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "BookingPassengerId",
                table: "Passengers");

            migrationBuilder.DropColumn(
                name: "BookingFlightId",
                table: "Flights");

            migrationBuilder.DropColumn(
                name: "BookingPassengerId",
                table: "Flights");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmationNumber",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationNumber",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "BookingFlightId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingPassengerId",
                table: "Passengers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingFlightId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingPassengerId",
                table: "Flights",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_BookingFlightId_BookingPassengerId",
                table: "Passengers",
                columns: new[] { "BookingFlightId", "BookingPassengerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_BookingFlightId_BookingPassengerId",
                table: "Flights",
                columns: new[] { "BookingFlightId", "BookingPassengerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Flights_Booking_BookingFlightId_BookingPassengerId",
                table: "Flights",
                columns: new[] { "BookingFlightId", "BookingPassengerId" },
                principalTable: "Booking",
                principalColumns: new[] { "FlightId", "PassengerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Passengers_Booking_BookingFlightId_BookingPassengerId",
                table: "Passengers",
                columns: new[] { "BookingFlightId", "BookingPassengerId" },
                principalTable: "Booking",
                principalColumns: new[] { "FlightId", "PassengerId" });
        }
    }
}
