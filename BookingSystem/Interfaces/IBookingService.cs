using BookingSystem.Dtos;

namespace BookingSystem.Interfaces
{
    public interface IBookingService
    {
        Task<List<BookDto>> GetBookedGuestHouesPerDays(int id);
    }
}
