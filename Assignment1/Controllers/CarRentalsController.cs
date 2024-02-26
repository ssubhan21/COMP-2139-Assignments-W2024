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
    public class CarRentalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarRentalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarRentals
        public async Task<IActionResult> Index()
        {
            return View(await _context.CarRentals.ToListAsync());
        }

        // GET: CarRentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarRentals
                .FirstOrDefaultAsync(m => m.CarRentalId == id);
            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }

        // GET: CarRentals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarRentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarRentalId,Model,RentalCompany,PricePerDay,Availability,Location,PickupDate,ReturnDate")] CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carRental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carRental);
        }

        // GET: CarRentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarRentals.FindAsync(id);
            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }

        // POST: CarRentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarRentalId,Model,RentalCompany,PricePerDay,Availability,Location,PickupDate,ReturnDate")] CarRental carRental)
        {
            if (id != carRental.CarRentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carRental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarRentalExists(carRental.CarRentalId))
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
            return View(carRental);
        }

        // GET: CarRentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _context.CarRentals
                .FirstOrDefaultAsync(m => m.CarRentalId == id);
            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }

        // POST: CarRentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var carRental = await _context.CarRentals.FindAsync(id);
            if (carRental != null)
            {
                _context.CarRentals.Remove(carRental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarRentalExists(int id)
        {
            return _context.CarRentals.Any(e => e.CarRentalId == id);
        }

        public async Task<IActionResult> CarSearch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CarSearchResult(string location, DateTime pickupDate, DateTime returnDate)
        {
            var searchResults = SearchCars(location, pickupDate, returnDate);
            return View("CarSearchResult", searchResults);
        }

        private List<CarRental> SearchCars(string location, DateTime pickupDate, DateTime returnDate)
        {
            // Implement your car search logic here
            // Query the database or external API to find matching cars
            // Example using Entity Framework Core:
            var cars = _context.CarRentals
                .Where(c => c.Location == location &&
                            c.PickupDate.Date == pickupDate.Date &&
                            c.ReturnDate.Date == returnDate.Date)
                .ToList();

            return cars;
        }

        public async Task<IActionResult> BookNow(int? id)
        {
            if (id == null || _context.CarRentals == null)
            {
                return NotFound();
            }

            var car = await _context.CarRentals.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }
    }
}
