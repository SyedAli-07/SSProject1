using SSProject1.Models;

namespace SSProject1.DTO
{
    public class BookingDTO
    {
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public int ConfirmationNumber { get; set; }

        //public int CurrentCapacity { get; set; }
        //public List<Flight> Flights { get; set; }
        //public  List<Passenger> Passengers { get; set; }
    }
}
