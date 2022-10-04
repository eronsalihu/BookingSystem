using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IBookingService
    {
        Task<Book> AddBookAsync(Book book);
        Task<List<Book>> GetBookedGuestHousePerDays(int id);
    }
}
