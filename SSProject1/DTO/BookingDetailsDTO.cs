using SSProject1.Models;

namespace SSProject1.DTO
{
    public class BookingDetailsDTO
    {
        public int Id { get; set; }
        public int PassengerCount { get; set; }
        public int FlightCount { get; set; }

        public List<Flight> Flights { get; set; }
        public List<Passenger> Passengers { get; set; }

    }
}
