namespace BookingSystem.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

    }
}
