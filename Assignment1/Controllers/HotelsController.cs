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
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HotelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hotels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotels.ToListAsync());
        }

        // GET: Hotels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // GET: Hotels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HotelId,Name,Location,PricePerNight,Rating,Availability,CheckInDate,CheckOutDate")] Hotels hotels)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotels);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotels);
        }

        // GET: Hotels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels.FindAsync(id);
            if (hotels == null)
            {
                return NotFound();
            }
            return View(hotels);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HotelId,Name,Location,PricePerNight,Rating,Availability,CheckInDate,CheckOutDate")] Hotels hotels)
        {
            if (id != hotels.HotelId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotels);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelsExists(hotels.HotelId))
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
            return View(hotels);
        }

        // GET: Hotels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotels = await _context.Hotels
                .FirstOrDefaultAsync(m => m.HotelId == id);
            if (hotels == null)
            {
                return NotFound();
            }

            return View(hotels);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotels = await _context.Hotels.FindAsync(id);
            if (hotels != null)
            {
                _context.Hotels.Remove(hotels);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelsExists(int id)
        {
            return _context.Hotels.Any(e => e.HotelId == id);
        }


        public IActionResult HotelSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult HotelSearchResult(string location, DateTime checkInDate, DateTime checkOutDate)
        {
            var searchResults = SearchHotels(location, checkInDate, checkOutDate);
            return View("HotelSearchResult", searchResults);
        }

        private List<Hotels> SearchHotels(string location, DateTime checkInDate, DateTime checkOutDate)
        {
            // Implement your hotel search logic here
            // Query the database or external API to find matching hotels
            // Example using Entity Framework Core:
            var hotels = _context.Hotels
                .Where(h => h.Location == location &&
                            h.CheckInDate.Date <= checkInDate.Date &&
                            h.CheckOutDate.Date >= checkOutDate.Date)
                .ToList();

            return hotels;
        }

        public async Task<IActionResult> BookNow(int? id)
        {
            if (id == null || _context.Hotels == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }
    }
}
