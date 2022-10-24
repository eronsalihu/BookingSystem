namespace BookingSystem.Dtos
{
    public class BookDto
    {
        public int Id { get; set; }
        public int GuestHouseId { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }
    }
}
