using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
