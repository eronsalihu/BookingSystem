using BookingSystem.Dtos;

namespace BookingSystem.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookDto>> GetBookingsByUserId(string userId);
        Task<List<BookDto>> GetBookedGuestHouesPerDays(int id);
    }
}
