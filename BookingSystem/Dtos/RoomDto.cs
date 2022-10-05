using BookingSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Dtos
{
    public class RoomDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Days { get; set; }

        [Required]
        public int NumberOfBeds { get; set; }

        [Required]
        public int GuestHouseId { get; set; }

        public List<AmenitiesEnum> Amenities { get; set; } = new List<AmenitiesEnum>();
    }
}