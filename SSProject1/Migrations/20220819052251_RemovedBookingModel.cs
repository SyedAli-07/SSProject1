using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSProject1.Migrations
{
    public partial class RemovedBookingModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flights_Booking_BookingFlightId_BookingPassengerId",
                table: "Flights");

            migrationBuilder.DropForeignKey(
                name: "FK_Passengers_Booking_BookingFlightId_BookingPassengerId",
                table: "Passengers");

            migrationBuilder.DropTable(
                name: "Booking");

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

            migrationBuilder.CreateTable(
                name: "FlightPassenger",
                columns: table => new
                {
                    FlightsId = table.Column<int>(type: "int", nullable: false),
                    PassengersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightPassenger", x => new { x.FlightsId, x.PassengersId });
                    table.ForeignKey(
                        name: "FK_FlightPassenger_Flights_FlightsId",
                        column: x => x.FlightsId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlightPassenger_Passengers_PassengersId",
                        column: x => x.PassengersId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlightPassenger_PassengersId",
                table: "FlightPassenger",
                column: "PassengersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FlightPassenger");

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

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    ConfirmationNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => new { x.FlightId, x.PassengerId });
                    table.ForeignKey(
                        name: "FK_Booking_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Booking_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Passengers_BookingFlightId_BookingPassengerId",
                table: "Passengers",
                columns: new[] { "BookingFlightId", "BookingPassengerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Flights_BookingFlightId_BookingPassengerId",
                table: "Flights",
                columns: new[] { "BookingFlightId", "BookingPassengerId" });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_PassengerId",
                table: "Booking",
                column: "PassengerId");

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
