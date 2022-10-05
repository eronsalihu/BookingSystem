using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IBookingService
    {
        Task<BookDto> AddBookAsync(Book book);
        Task<List<BookDto>> GetBookedGuestHousePerDays(int id);
    }
}
