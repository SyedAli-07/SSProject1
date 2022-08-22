using SSProject1.Models;

namespace SSProject1.DTO
{
    public class PassengerDetailsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }

        public List<Flight> FlightsList { get; set; }
    }
}
