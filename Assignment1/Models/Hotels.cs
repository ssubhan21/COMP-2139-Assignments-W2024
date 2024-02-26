using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Hotels
    {
        [Key]
        public int HotelId { get; set; }

        public string? Name { get; set; }
       
        public string? Location { get; set; }

        public decimal PricePerNight { get; set; }

        public int Rating { get; set; }

        public bool Availability { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
