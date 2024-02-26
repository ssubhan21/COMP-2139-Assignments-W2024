using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Bookings
    {

        [Key]
        public int BookingId { get; set; }

        public int? UserId { get; set; } // Nullable int to allow unregistered users
        public int FlightId { get; set; }
        public int HotelId { get; set; }
        public int CarRentalId { get; set; }
        public string Email { get; set; }
        public DateTime BookingDate { get; set; }

    }
}
