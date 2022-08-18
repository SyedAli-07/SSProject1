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
        public DbSet<Booking> Booking { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Booking>()
            //    .HasKey(bk => bk.Id);


            //// setting up one to many relationship between booking and flights
            //modelBuilder.Entity<Booking>()
            //    .HasOne(bk => bk.Flights)
            //    .WithMany(p => p.)
            //    .HasForeignKey(ps => ps.FlightId);

            //// setting up one to many relationship between booking and passengers
            //modelBuilder.Entity<Booking>()
            //    .HasOne(ps => ps.Passenger)
            //    .WithMany(p => p.BookedFlights)
            //    .HasForeignKey(ps => ps.PassengerId);

            // Another way to write the above code

            modelBuilder.Entity<Booking>(booking =>
            {
                booking.HasKey(booking => booking.Id);

                booking.HasOne(booking => booking.Booked)
                .WithMany(f => f.Flights)
                .HasForeignKey(booking => booking.FlightId);

                booking.HasOne(booking => booking.Passenger)
                .WithMany(booking => booking.BookedFlights)
                .HasForeignKey(booking => booking.PassengerId);

            });
        }

        
    }
}
