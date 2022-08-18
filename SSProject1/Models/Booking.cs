using SSProject1.DTO;

namespace SSProject1.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }

        //public virtual Flight Flight { get; set; }

        //public virtual Passenger Passenger { get; set; }

        //public virtual Booking Booked { get; set; }

        // Not Stored Property
        public int CurrentCapacity => Passengers?.Count ?? 0;

        public virtual ICollection<Flight> Flights { get; set; }
        public virtual ICollection<Passenger> Passengers { get; set; }

        public Booking() { }

        public Booking(BookingDTO dto)
        {    
            this.FlightId = dto.FlightId;
            this.PassengerId = dto.PassengerId;
            this.Flights = new List<Flight>();
            this.Passengers = new List<Passenger>();
        }
    }
}
