using System.ComponentModel.DataAnnotations;

namespace SSProject1.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureDateTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalDateTime { get; set; }
        public string ArrivalAirport { get; set; }
        public int MaxCapacity { get; set; }


        // Navigation Properties
        public virtual ICollection<Booking> BookedPassengers { get; set; }

    }
}
