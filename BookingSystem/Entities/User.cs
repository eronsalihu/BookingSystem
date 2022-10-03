using Microsoft.AspNetCore.Identity;

namespace BookingSystem.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
    }
}
