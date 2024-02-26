using Assignment1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace Assignment1.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Flights> Flights { get; set; }
        public DbSet<Hotels> Hotels { get; set; }
        public DbSet<CarRental> CarRentals { get; set; }
        public DbSet<Assignment1.Models.Bookings> Bookings { get; set; } = default!;


    }
}
