using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSProject1.Data;
using SSProject1.Models;
using SSProject1.DTO;

namespace SSProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly FlightDbContext _context;
        private readonly ILogger<BookingsController> _logger;

        public BookingsController(ILogger<BookingsController> logger, FlightDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBooking()
        {
          if (_context.Booking == null)
          {
              return NotFound();
          }
            return await _context.Booking.ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{flightId}/{passengerId}")]
        public async Task<ActionResult<BookingDTO>> GetBooking(int flightId, int passengerId)
        {
          if (_context.Booking == null)
          {
              return NotFound();
          }
            var booking = await _context.Booking.FindAsync(flightId, passengerId);

            if (booking == null)
            {
                return NotFound();
            }

            var flights = await _context.Flights.Where(f => f.BookedPassengers.Where(fp => fp.FlightId == booking.FlightId).Any()).ToListAsync();
            var passengers = await _context.Passengers.Where(p => p.BookedFlights.Where(pf => pf.PassengerId == booking.PassengerId).Any()).ToListAsync();

            var fpDTO = new BookingDTO
            {
                FlightId = booking.FlightId,
                PassengerId = booking.PassengerId,
                ConfirmationNumber = booking.ConfirmationNumber
                //Flights = flights,
                //Passengers = passengers
            };

            return Ok(fpDTO);
        }

        // PUT: api/Bookings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.FlightId)
            {
                return BadRequest();
            }

            _context.Entry(booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bookings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(BookingDTO bookingDto)
        {
          if (_context.Booking == null)
          {
              return Problem("Entity set 'FlightDbContext.Booking'  is null.");
          }
            var booking = new Booking(bookingDto);
            _context.Booking.Add(booking);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) 
            {
                if (BookingExists(bookingDto.FlightId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBooking", new { id = bookingDto.FlightId, bookingDto.PassengerId }, bookingDto);
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{flightId}/{passengerId}")]
        public async Task<IActionResult> DeleteBooking(int flightId, int passengerId)
        {
            if (_context.Booking == null)
            {
                return NotFound();
            }
            var booking = await _context.Booking.FindAsync(flightId, passengerId);
            if (booking == null)
            {
                return NotFound();
            }

            _context.Booking.Remove(booking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookingExists(int id)
        {
            return (_context.Booking?.Any(e => e.FlightId == id)).GetValueOrDefault();
        }
    }
}
