using SSProject1.Models;

namespace SSProject1.DTO
{
    public class FlightDTO
    {
        public string FlightNumber { get; set; }
        public string DepartureDateTime { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalDateTime { get; set; }
        public string ArrivalAirport { get; set; }
        public int MaxCapacity { get; set; }

    }
}
