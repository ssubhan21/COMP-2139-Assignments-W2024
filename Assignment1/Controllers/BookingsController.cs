using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assignment1.Data;
using Assignment1.Models;

namespace Assignment1.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,UserId,FlightId,HotelId,CarRentalId,Email,BookingDate")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bookings);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,UserId,FlightId,HotelId,CarRentalId,Email,BookingDate")] Bookings bookings)
        {
            if (id != bookings.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingsExists(bookings.BookingId))
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
            return View(bookings);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }

            return View(bookings);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            if (bookings != null)
            {
                _context.Bookings.Remove(bookings);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingsExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

        [HttpGet]
        public IActionResult Confirmation(int bookingId)
        {
            var booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);

            if (booking == null)
            {
                // Handle the case where no booking is found with the specified bookingId
                return View("BookingNotFound");
            }

            // Pass the booking details to the Confirmation view
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Confirmation(string email, int? flightId, int? hotelId, int? carId)
        {
            if (flightId.HasValue)
            {
                var flightDetails = _context.Flights.FirstOrDefault(f => f.FlightId == flightId.Value);
                if (flightDetails != null)
                {
                    var flightBooking = new Bookings
                    {
                        Email = email,
                        FlightId = flightId.Value,
                        // Add other properties as needed
                    };

                    _context.Add(flightBooking);
                    await _context.SaveChangesAsync();

                    // Redirect to the Confirmation view with the booking details
                    return RedirectToAction("Confirmation", new { bookingId = flightBooking.BookingId });
                }
            }
            else if (hotelId.HasValue)
            {
                var hotelDetails = _context.Hotels.FirstOrDefault(h => h.HotelId == hotelId.Value);
                if (hotelDetails != null)
                {
                    var hotelBooking = new Bookings
                    {
                        Email = email,
                        HotelId = hotelId.Value,
                        // Add other properties as needed
                    };

                    _context.Add(hotelBooking);
                    await _context.SaveChangesAsync();

                    // Redirect to the Confirmation view with the booking details
                    return RedirectToAction("Confirmation", new { bookingId = hotelBooking.BookingId });
                }
            }
            else if (carId.HasValue)
            {
                var carDetails = _context.CarRentals.FirstOrDefault(c => c.CarRentalId == carId.Value);
                if (carDetails != null)
                {
                    var carBooking = new Bookings
                    {
                        Email = email,
                        CarRentalId = carId.Value,
                        // Add other properties as needed
                    };

                    _context.Add(carBooking);
                    await _context.SaveChangesAsync();

                    // Redirect to the Confirmation view with the booking details
                    return RedirectToAction("Confirmation", new { bookingId = carBooking.BookingId });
                }
            }

            else
            {
                // Handle the case where none of the IDs are provided or are invalid
                return RedirectToAction("Error");
            }

            // Handle the case where the item type is not recognized or is null
            return RedirectToAction("Error");
        }


    }
}
