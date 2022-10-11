using BookingSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Data.Identity
{
    public class IdentityContextSeed
    {
        public static async Task SeedUserAsync(UserManager<User> userManager)
        {
            var user = new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin",
                Email = "admin@gmail.com",
                PhoneNumber = "123456789",
                Role = Role.Admin,
            };

            await userManager.CreateAsync(user, "@Admin123");
            await userManager.AddToRoleAsync(user, Role.User);
        }
    }
}
