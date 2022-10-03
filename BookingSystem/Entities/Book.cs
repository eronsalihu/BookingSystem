namespace BookingSystem.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public GuestHouse GuestHouse { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }

    }
}
