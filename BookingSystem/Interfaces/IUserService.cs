using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        User UpdateUser(User user);
    }
}
