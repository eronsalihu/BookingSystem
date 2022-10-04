using BookingSystem.Entities;

namespace BookingSystem.Dtos
{
    public class BookDto
    {
        public int GuestHouseId { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }
    }
}
