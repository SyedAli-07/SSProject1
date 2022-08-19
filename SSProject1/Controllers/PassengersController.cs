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
    public class PassengersController : ControllerBase
    {
        private readonly FlightDbContext _context;
        private readonly ILogger<PassengersController> _logger;

        public PassengersController(ILogger<PassengersController> logger ,FlightDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Passengers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Passenger>>> GetPassengers()
        {
          if (_context.Passengers == null)
          {
              return NotFound();
          }
            return await _context.Passengers.ToListAsync();
        }

        // GET: api/Passengers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Passenger>> GetPassenger(int id)
        {
          if (_context.Passengers == null)
          {
              return NotFound();
          }
            var passenger = await _context.Passengers.FindAsync(id);

            if (passenger == null)
            {
                return NotFound();
            }

            return passenger;
        }

        // GET: api/Passengers/Flight/5
        [HttpGet("Flight/{id}")]
        public async Task<ActionResult<List<PassengerDTO>>> GetPassengersByFlight(int id)
        {
            if (_context.Passengers == null)
            {
                return NotFound();
            }
            var flight = await _context.Flights.Include(p => p.Passengers).FirstOrDefaultAsync(f => f.Id == id);
            var fp = flight.Passengers;
            var pDto = new List<PassengerDTO>();
            foreach(var f in fp)
            {
                var d = new PassengerDTO
                {
                    Id = f.Id,
                    Name = f.Name,
                    Age = f.Age,
                    Occupation = f.Occupation,
                    Email = f.Email
                };
                pDto.Add(d);
            }

            return Ok(pDto);
        }

        // PUT: api/Passengers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPassenger(int id, Passenger passenger)
        {
            if (id != passenger.Id)
            {
                return BadRequest();
            }

            _context.Entry(passenger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, "An error has occured while attempting to update a passenger's information.");
                if (!PassengerExists(id))
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

        // POST: api/Passengers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Passenger>> PostPassenger(PassengerDTO passengerDto)
        {
          if (_context.Passengers == null)
          {
              return Problem("Entity set 'FlightDbContext.Passengers'  is null.");
          }

            var passenger = new Passenger()
            {
                Name = passengerDto.Name,
                Age = passengerDto.Age,
                Occupation = passengerDto.Occupation,
                Email = passengerDto.Email
            };
            _context.Passengers.Add(passenger);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPassenger", new { id = passenger.Id }, passenger);

        }

        // POST: api/Passengers/id/Flight/flightId
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/Flight/{flightId}")]
        public async Task<ActionResult<Passenger>> PostFlightToPassenger(int id, int flightId)
        {
            if (_context.Passengers == null)
            {
                return Problem("Entity set 'FlightDbContext.Passengers'  is null.");
            }

            var passenger = await _context.Passengers.FindAsync(id);
            var flight = await _context.Flights.FindAsync(flightId);

            passenger.Flights.Add(flight);

            await _context.SaveChangesAsync();

            return Ok();
        }
            // DELETE: api/Passengers/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassenger(int id)
        {
            if (_context.Passengers == null)
            {
                return NotFound();
            }
            var passenger = await _context.Passengers.FindAsync(id);
            if (passenger == null)
            {
                return NotFound();
            }

            _context.Passengers.Remove(passenger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PassengerExists(int id)
        {
            return (_context.Passengers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
