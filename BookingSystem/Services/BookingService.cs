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
            if (await _context.GuestHouses.SingleOrDefaultAsync(e => e.Id == book.GuestHouseId) == null)
            {

                throw new KeyNotFoundException($"No guesthouse found with id: {book.GuestHouseId}");

            }
            await _context.AddAsync(book);
            await _context.SaveChangesAsync();
            return new BookDto
            {
                Id = book.Id,
                GuestHouseId = book.GuestHouseId,
                BookFrom = book.BookFrom,
                BookTo = book.BookTo
            };
        }

        public async Task<IEnumerable<BookDto>> GetBookedGuestHousePerDays(int id) =>
            await _context.Bookings.Where(e => e.GuestHouseId == id).Select(e => new BookDto
            {
                Id = e.Id,
                GuestHouseId = e.GuestHouseId,
                BookFrom = e.BookFrom,
                BookTo = e.BookTo,
            }).ToListAsync();

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
