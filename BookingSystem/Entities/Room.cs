namespace BookingSystem.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Day { get; set; }
        public int NumberOfBeds { get; set; }
        /*public AmenitiesEnum Amenities { get; set; }*/
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
