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


		public async Task<List<Book>> GetBookedGuestHouesPerDays(int id) =>
			await _context.Bookings.Include(e => e.Room).Where(e => e.Room.Id == id).ToListAsync();


		public async Task<List<BookDto>> GetBookingsByUserId(string userId) =>
			await _context.Bookings.Include(x => x.Room).Where(e => e.CreatedBy == userId).Select(e => new BookDto
			{
				Id = e.Id,
				RoomId = e.RoomId,
				BookFrom = e.BookFrom,
				BookTo = e.BookTo,
				Room = new RoomDto
				{
					Id = e.Id,
					Name = e.Room.Name,
					Image = e.Room.Image,
					Description = e.Room.Description,
					Price = e.Room.Price,
					NumberOfBeds = e.Room.NumberOfBeds,
					GuestHouseId = e.Room.GuestHouseId,
					Amenities = e.Room.Amenities.Select(a => a.Amenities).ToList()

				}
			}).ToListAsync();
	}
}
