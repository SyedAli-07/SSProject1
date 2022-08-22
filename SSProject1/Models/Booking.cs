using SSProject1.DTO;
using System.Text.Json.Serialization;
namespace SSProject1.Models
{
    public class Booking
    {
        //public int Id { get; set; }
        public int FlightId { get; set; }
        [JsonIgnore]
        public virtual Flight Flight { get; set; }
        public int PassengerId { get; set; }
        [JsonIgnore]
        public virtual Passenger Passenger { get; set; }
        public int ConfirmationNumber { get; set; }

        // Not Stored Property
        //public int CurrentCapacity => BookedPassengers?.Count ?? 0;

        //public virtual ICollection<Flight> BookedPassengers { get; set; }
        //public virtual ICollection<Passenger> BookedFlights { get; set; }

        public Booking() { }

        public Booking(BookingDTO dto)
        {
            this.FlightId = dto.FlightId;
            this.PassengerId = dto.PassengerId;
            this.ConfirmationNumber = dto.ConfirmationNumber;
        //this.Flights = new List<Flight>();
        //this.Passengers = new List<Passenger>();
    }
    }
}
