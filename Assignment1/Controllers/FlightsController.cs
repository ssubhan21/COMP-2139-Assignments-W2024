using Assignment1.Data;
using Assignment1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment1.Controllers
{
    public class FlightsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public FlightsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Flights
        public async Task<IActionResult> Index()
        {
            return _context.Flights != null ?
                        View(await _context.Flights.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
        }

        // GET: Flights/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flights = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flights == null)
            {
                return NotFound();
            }

            return View(flights);
        }

        // GET: Flights/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Flights/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlightId,Airline,DepartureCity,ArrivalCity,DepartureTime,ArrivalTime,Price")] Flights flights)
        {
            if (ModelState.IsValid)
            {
                _context.Add(flights);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flights);
        }

        // GET: Flights/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flights = await _context.Flights.FindAsync(id);
            if (flights == null)
            {
                return NotFound();
            }
            return View(flights);
        }

        // POST: Flights/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FlightId,Airline,DepartureCity,ArrivalCity,DepartureTime,ArrivalTime,Price")] Flights flights)
        {
            if (id != flights.FlightId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flights);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightsExists(flights.FlightId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(flights);
        }

        // GET: Flights/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Flights == null)
            {
                return NotFound();
            }

            var flights = await _context.Flights
                .FirstOrDefaultAsync(m => m.FlightId == id);
            if (flights == null)
            {
                return NotFound();
            }

            return View(flights);
        }

        // POST: Flights/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Flights == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Flights'  is null.");
            }
            var flights = await _context.Flights.FindAsync(id);
            if (flights != null)
            {
                _context.Flights.Remove(flights);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> FlightSearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FlightSearchResult(string departureCity, string arrivalCity, DateTime departureDate)
        {
            var searchResults = SearchFlights(departureCity, arrivalCity, departureDate);
            return View("FlightSearchResult", searchResults);
        }

        private List<Flights> SearchFlights(string departureCity, string arrivalCity, DateTime departureDate)
        {
            // Implement your flight search logic here
            // Query the database or external API to find matching flights
            // Example using Entity Framework Core:
            var flights = _context.Flights
                .Where(f => f.DepartureCity == departureCity &&
                            f.ArrivalCity == arrivalCity &&
                            f.DepartureTime.Date == departureDate.Date)
                .ToList();

            return flights;
        }

        public IActionResult BookNow(int flightId)
        {
            // Retrieve flight information based on the flightId
            var flight = _context.Flights.FirstOrDefault(f => f.FlightId == flightId);

            if (flight == null)
            {
                return NotFound(); // Handle the case where the flight is not found
            }

            return View(flight);
        }



        private bool FlightsExists(int id)
        {
            return (_context.Flights?.Any(e => e.FlightId == id)).GetValueOrDefault();
        }


    }
}
