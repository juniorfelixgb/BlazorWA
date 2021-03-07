using System.ComponentModel.DataAnnotations;

namespace HotelReservations.Shared.Models
{
    public class Reservation
    {
        public int Id { get; set; }
 
        [Required]
        public string Name { get; set; }
 
        [Required]
        public string StartLocation { get; set; }
 
        [Required]
        public string EndLocation { get; set; }
    }
}