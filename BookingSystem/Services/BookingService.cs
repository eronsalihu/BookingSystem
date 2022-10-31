using BookingSystem.Data;
using BookingSystem.Dtos;
using BookingSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
    public class BookingService : IBookingService
    {
        private readonly BookingContext _context;

        public BookingService(BookingContext context)
        {
            _context = context;
        }


        public async Task<List<BookDto>> GetBookedGuestHouesPerDays(int id)
        {

            return await _context.Bookings.Where(e => e.Room.GuestHouseId == id).Select(e => new BookDto
            {
                Id = e.Id,
                RoomId = e.RoomId,
                BookFrom = e.BookFrom,
                BookTo = e.BookTo,
            }).ToListAsync();
        }
    }
}
