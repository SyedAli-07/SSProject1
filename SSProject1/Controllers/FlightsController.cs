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
    public class FlightsController : ControllerBase
    {
        private readonly FlightDbContext _context;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(ILogger<FlightsController> logger, FlightDbContext context)
        {
            _logger = logger;   
            _context = context;
        }

        // GET: api/Flights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flight>>> GetFlights()
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            return await _context.Flights.ToListAsync();
        }

        // GET: api/Flights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
          if (_context.Flights == null)
          {
              return NotFound();
          }
            var flight = await _context.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }


        // GET: api/Flights/Passenger/5
        [HttpGet("Flight/{id}")]
        public async Task<ActionResult<List<FlightDTO>>> GetFlightsByPassenger(int id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            var passenger = await _context.Passengers.Include(p => p.Flights).FirstOrDefaultAsync(f => f.Id == id);
            var fp = passenger.Flights;
            var pDto = new List<FlightDTO>();
            foreach (var f in fp)
            {
                var d = new FlightDTO
                {
                    Id = f.Id,
                    DepartureDateTime = f.DepartureDateTime,
                    DepartureAirport = f.DepartureAirport,
                    ArrivalDateTime = f.ArrivalDateTime,
                    ArrivalAirport = f.ArrivalAirport,
                    MaxCapacity = f.MaxCapacity
                };
                pDto.Add(d);
            }

            return Ok(pDto);
        }

        // PUT: api/Flights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.Id)
            {
                return BadRequest();
            }

            _context.Entry(flight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "An error occured while attempting to update a flight's information. ");
                if (!FlightExists(id))
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

        // POST: api/Flights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Flight>> PostFlight(FlightDTO flightDto)
        {
          if (_context.Flights == null)
          {
              return Problem("Entity set 'FlightDbContext.Flights'  is null.");
          }

            var flight = new Flight()
            {
                FlightNumber = flightDto.FlightNumber,
                DepartureDateTime = flightDto.DepartureDateTime,
                DepartureAirport = flightDto.DepartureAirport,
                ArrivalDateTime = flightDto.ArrivalDateTime,
                ArrivalAirport = flightDto.ArrivalAirport,
                MaxCapacity = flightDto.MaxCapacity
            };

            _context.Flights.Add(flight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlight", new { id = flight.Id }, flight);
        }

        // POST: api/Flights/id/Passenger/passengerId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/Passenger/{passengerId}")]
        public async Task<ActionResult<Flight>> PostFlightToPassenger(int id, int passengerId)
        {
            if (_context.Passengers == null)
            {
                return Problem("Entity set 'FlightDbContext.Flights'  is null.");
            }

            var flight = await _context.Flights.FindAsync(id);
            var passenger = await _context.Passengers.FindAsync(passengerId);

            flight.Passengers.Add(passenger);

            await _context.SaveChangesAsync();

            return Ok();
        }
            // DELETE: api/Flights/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            if (_context.Flights == null)
            {
                return NotFound();
            }
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null)
            {
                return NotFound();
            }

            _context.Flights.Remove(flight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlightExists(int id)
        {
            return (_context.Flights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
