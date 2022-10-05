using BookingSystem.Data;
using BookingSystem.Dtos;
using BookingSystem.Entities;
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
        public async Task<BookDto> AddBookAsync(Book book)
        {
            if (await _context.GuestHouses.SingleOrDefaultAsync(e => e.Id == book.GuestHouseId) == null) return null;
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return new BookDto
            {
                BookFrom = book.BookFrom,
                BookTo = book.BookTo
            };
        }

        public async Task<List<BookDto>> GetBookedGuestHousePerDays(int id) =>
            await _context.Bookings.Where(e => e.GuestHouseId == id).Select(e => new BookDto
            {
                BookFrom = e.BookFrom,
                BookTo = e.BookTo,
            }).ToListAsync();

    }
}
