using Microsoft.EntityFrameworkCore;

using SSProject1.Models;

namespace SSProject1.Data
{
    public class FlightDbContext : DbContext
    {
        public FlightDbContext(DbContextOptions<FlightDbContext> options) : base(options)
        {

        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasKey(booking => new {booking.FlightId, booking.PassengerId});


            // setting up one to many relationship between booking and flights
            modelBuilder.Entity<Booking>()
                .HasOne(ps => ps.Flight)
                .WithMany(p => p.BookedPassengers)
                .HasForeignKey(ps => ps.FlightId);

            // setting up one to many relationship between booking and passengers
            modelBuilder.Entity<Booking>()
                .HasOne(ps => ps.Passenger)
                .WithMany(p => p.BookedFlights)
                .HasForeignKey(ps => ps.PassengerId);

            // Another way to write the above code

            //modelBuilder.Entity<Booking>(booking =>
            //{
            //    booking.HasKey(booking => new { booking.FlightId, booking.PassengerId });

            //    booking.HasOne(booking => booking.Flight)
            //    .WithMany(booking => booking.BookedPassengers)
            //    .HasForeignKey(booking => booking.FlightId);
                
            //    booking.HasOne(booking => booking.Passenger)
            //    .WithMany(booking => booking.BookedFlights)
            //    .HasForeignKey(booking => booking.PassengerId);

            //});
        }

        public DbSet<SSProject1.Models.Booking>? Booking { get; set; }
    }
}
