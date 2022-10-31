using System.ComponentModel.DataAnnotations;
namespace BookingSystem.Dtos
{
    public class GuestHouseDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

    }
}
