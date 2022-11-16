using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookDto>> GetBookingsByUserId(string userId);
        Task<List<Book>> GetBookedGuestHouesPerDays(int id);
    }
}
