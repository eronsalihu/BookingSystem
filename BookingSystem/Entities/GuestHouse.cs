namespace BookingSystem.Entities
{
    public class GuestHouse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Book> Books { get; set; }
    }
}
