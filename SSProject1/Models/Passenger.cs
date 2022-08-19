using System.ComponentModel.DataAnnotations;

namespace SSProject1.Models
{
    public class Passenger
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }

        // Navigation Properties
        public virtual ICollection<Flight> Flights { get; set; }
    }
}
