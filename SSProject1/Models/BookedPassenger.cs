namespace SSProject1.Models
{
    public class BookedPassenger
    {
        public int Id { get; set; }

        public int PassengerId { get; set; }

        public virtual Passenger Passenger { get; set; }

        public int BookingId { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
