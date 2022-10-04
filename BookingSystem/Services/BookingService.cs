using BookingSystem.Data;
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
        public async Task<Book> AddBookAsync(Book book)
        {
            if (await _context.GuestHouses.SingleOrDefaultAsync(e => e.Id == book.GuestHouseId) == null) return null;
            await _context.AddAsync(book);
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Bookings ON");
            await _context.SaveChangesAsync();
            await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Bookings OFF");
            return book;
        }

        public async Task<List<Book>> GetBookedGuestHousePerDays(int id) =>
            await _context.Bookings.Where(e => e.GuestHouseId == id).Select(e => new Book
            {
                GuestHouse = e.GuestHouse,
                BookFrom = e.BookFrom,
                BookTo = e.BookTo,
            }).ToListAsync();

    }
}
