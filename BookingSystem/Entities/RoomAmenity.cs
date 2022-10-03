namespace BookingSystem.Entities
{
    public class RoomAmenity
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public AmenitiesEnum Amenities { get; set; }
    }
}
