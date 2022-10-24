using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IBookingService
    {
        Task<IEnumerable<BookDto>> GetBookingsAsync();
        Task<BookDto> AddBookAsync(Book book);
        Task<IEnumerable<BookDto>> GetBookedGuestHousePerDays(int id);
        Task<BookDto> GetBookingByIdAsync(int id);
    }
}
