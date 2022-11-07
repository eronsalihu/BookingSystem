using BookingSystem.Data.Identity;
using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IdentityContext _context;

        public UserService(IdentityContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(string id) =>
            await _context.Users.SingleOrDefaultAsync(e => e.Id == id);

        public async Task<IEnumerable<User>> GetUsersAsync() =>
            await _context.Users.ToListAsync();
        public  User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return user;
        }

    }
}
