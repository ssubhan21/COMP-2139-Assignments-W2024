using Microsoft.CodeAnalysis;
using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class CarRental
    {
        public int CarRentalId { get; set; }

        [Required(ErrorMessage = "Model is required")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "RentalCompany is required")]
        public string? RentalCompany { get; set; }
        public decimal PricePerDay { get; set; }

        public bool Availability { get; set; }

        public string? Location {get; set;}

        public DateTime PickupDate { get; set; } // New property for pickup date
        public DateTime ReturnDate { get; set; } // New property for return date

    }
}
