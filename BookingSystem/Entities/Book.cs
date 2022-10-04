using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookingSystem.Entities
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int GuestHouseId { get; set; }
        public GuestHouse GuestHouse { get; set; }
        public DateTime BookFrom { get; set; }
        public DateTime BookTo { get; set; }

    }
}
