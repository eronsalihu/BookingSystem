using BookingSystem.Data;
using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
	public class RoomService : IRoomService
	{
		private readonly BookingContext _context;

		public RoomService(BookingContext context)
		{
			_context = context;
		}

		public async Task<RoomDto> AddRoomsAsync(Room room)
		{
			await _context.Rooms.AddAsync(room);

			await _context.SaveChangesAsync();

			var roomDto = new RoomDto
			{
				Id = room.Id,
				Name = room.Name,
				Image = room.Image,
				Description = room.Description,
				Price = room.Price,
				NumberOfBeds = room.NumberOfBeds,
				GuestHouseId = room.GuestHouseId,
				Amenities = room.Amenities.Select(a => a.Amenities).ToList()
			};

			return roomDto;

		}

		public async Task<RoomDto> UpdateRoomAsync(Room room)
		{
			if (await _context.Rooms.AsNoTracking().SingleOrDefaultAsync(e => e.Id == room.Id) == null)
			{
				throw new KeyNotFoundException($"No room found with id: {room.Id}");
			}
			var roomAmenities = await _context.RoomAmenities.Where(e => e.RoomId == room.Id).ToListAsync();

			var except = roomAmenities.Except(room.Amenities);

			_context.RemoveRange(except);

			_context.Rooms.Update(room);
			await _context.SaveChangesAsync();

			return new RoomDto
			{
				Id = room.Id,
				Name = room.Name,
				Description = room.Description,
				Image = room.Image,
				Price = room.Price,
				NumberOfBeds = room.NumberOfBeds,
				GuestHouseId = room.GuestHouseId,
				Amenities = room.Amenities.Select(e => e.Amenities).ToList(),
			};
		}

		public async Task<List<RoomDto>> GetRoomsByGuestHouseId(int guestHouseId, DateTime? checkIn, DateTime? checkOut)
		{
			if (checkIn != null && checkOut != null)
			{
				var foundBookings = _context.Bookings.Where(
						bookings => !(DateTime.Compare(bookings.BookFrom, (DateTime)checkIn) <= 0 &&
									DateTime.Compare(bookings.BookTo, (DateTime)checkOut) >= 0));

				return await _context.Rooms.Include(x => x.Books).Where(e => !foundBookings.Any(fb => fb.RoomId == e.Id) && e.GuestHouseId == guestHouseId).Select(e => new RoomDto
				{
					Id = e.Id,
					Name = e.Name,
					Image = e.Image,
					Description = e.Description,
					Price = e.Price,
					NumberOfBeds = e.NumberOfBeds,
					GuestHouseId = e.GuestHouseId,
					Amenities = e.Amenities.Select(a => a.Amenities).ToList()

				}).ToListAsync();
			}
			else
			{
				return await _context.Rooms.Include(x => x.Books).Where(e => e.GuestHouseId == guestHouseId).Select(e => new RoomDto
				{
					Id = e.Id,
					Name = e.Name,
					Image = e.Image,
					Description = e.Description,
					Price = e.Price,
					NumberOfBeds = e.NumberOfBeds,
					GuestHouseId = e.GuestHouseId,
					Amenities = e.Amenities.Select(a => a.Amenities).ToList()

				}).ToListAsync();
			}


		}

		public void DeleteRoomAsync(int id)
		{
			var room = _context.Rooms.SingleOrDefault(e => e.Id == id);

			if (room == null)
			{
				throw new KeyNotFoundException($"No room found with id: {id}");
			}
			_context.Rooms.Remove(room);
			_context.SaveChanges();
		}

		public RoomDto GetRoomById(int id)
		{
			var room = _context.Rooms.Find(id);
			if (room == null)
			{
				throw new KeyNotFoundException($"No room found with id: {id}");
			}

			return new RoomDto
			{
				Id = id,
				Name = room.Name,
				Description = room.Description,
				Price = room.Price,
				Image = room.Image,
				NumberOfBeds = room.NumberOfBeds,
				GuestHouseId = room.GuestHouseId,
				Amenities = _context.RoomAmenities.Where(e => e.RoomId == room.Id).Select(e => e.Amenities).ToList() ?? null,
			};
		}

		public async Task<BookDto> BookRoomAsync(Book book)
		{
			await _context.Bookings.AddAsync(book);
			await _context.SaveChangesAsync();

			return new BookDto
			{
				Id = book.Id,
				RoomId = book.RoomId,
				BookFrom = book.BookFrom,
				BookTo = book.BookTo,
			};
		}
	}
}
