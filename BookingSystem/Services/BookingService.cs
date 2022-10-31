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
                RoomId = e.RoomId,
                BookFrom = e.BookFrom,
                BookTo = e.BookTo,
            }).ToListAsync();
        }

        public async Task<BookDto> GetBookingByIdAsync(int id)
        {
            if (await _context.Bookings.SingleOrDefaultAsync(e => e.Id == id) == null)
            {
                throw new KeyNotFoundException($"No booking were found with id: {id}");
            }
            return await _context.Bookings.Where(e => e.Id == id).Select(e => new BookDto
            {
                Id = e.Id,
                GuestHouseId = e.GuestHouseId,
                BookFrom = e.BookFrom,
                BookTo = e.BookTo
            }).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookDto>> GetBookingsAsync() =>
            await _context.Bookings.Select(e => new BookDto
            {
                Id = e.Id,
                GuestHouseId = e.GuestHouseId,
                BookFrom = e.BookFrom,
                BookTo = e.BookTo

            }).ToListAsync();
    }
}
