using BookingSystem.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Dtos
{
	public class RoomDto
	{
		public int Id { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public string Description { get; set; }

		[Required]
		public byte[]? Image { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		public int NumberOfBeds { get; set; }

		[Required]
		public int GuestHouseId { get; set; }

		public List<AmenitiesEnum> Amenities { get; set; } = new List<AmenitiesEnum>();
	}
}