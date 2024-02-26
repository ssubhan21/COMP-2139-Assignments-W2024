using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Flights
    {
        [Key]
        [Required]
        public int FlightId { get; set; }

        public string? Airline { get; set; }

        public string? DepartureCity { get; set; }

        public string? ArrivalCity { get; set; }

        [DataType(DataType.Date)]
        public DateTime DepartureTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime ArrivalTime { get; set; }

        public decimal Price { get; set; }

    }
}
