namespace SSProject1.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }

        // Navigation Properties
        public virtual ICollection<Booking> BookedFlights { get; set; }
    }
}
