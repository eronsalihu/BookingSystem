using BookingSystem.Data;
using BookingSystem.Data.Identity;
using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Services
{
	public class GuestHouseService : IGuestHouseService
	{
		private readonly BookingContext _context;
		private readonly IdentityContext _identityContext;

		public GuestHouseService(BookingContext context, IdentityContext identityContext)
		{
			_context = context;
			_identityContext = identityContext;
		}

		public async Task<GuestHouseDto> AddGuestHouseAsync(GuestHouse guestHouse)
		{
			await _context.GuestHouses.AddAsync(guestHouse);
			await _context.SaveChangesAsync();
			return new GuestHouseDto
			{
				Id = guestHouse.Id,
				Name = guestHouse.Name,
				Description = guestHouse.Description,
			};
		}

		public async Task<List<GuestHouseDto>> GetAllGuestHousesAsync()
		{
			return await _context.GuestHouses.Select(e => new GuestHouseDto
			{
				Id = e.Id,
				Name = e.Name,
				Description = e.Name
			}).ToListAsync();

		}

		public async Task<GuestHouseDto> UpdateGuestHouseAsync(GuestHouse guestHouse)
		{
			if (await _context.GuestHouses.AsNoTracking().SingleOrDefaultAsync(e => e.Id == guestHouse.Id) == null)
			{
				throw new KeyNotFoundException($"No guesthouse found with id: {guestHouse.Id}");
			}

			_context.GuestHouses.Update(guestHouse);
			await _context.SaveChangesAsync();
			return new GuestHouseDto
			{
				Id = guestHouse.Id,
				Name = guestHouse.Name,
				Description = guestHouse.Description,
			}; ;
		}

		public void DeleteGuestHouseAsync(int id)
		{
			var guestHouse = _context.GuestHouses.SingleOrDefault(e => e.Id == id);
			if (guestHouse == null)
			{
				throw new KeyNotFoundException($"No guesthouse found with id: {id}");
			}
			_context.GuestHouses.Remove(guestHouse);
			_context.SaveChanges();

		}

		public GuestHouseDto GetGuestHousesById(int id)
		{
			var guestHouse = _context.GuestHouses.Find(id);
			if (guestHouse == null)
			{
				throw new KeyNotFoundException($"No guesthouse found with id: {id}");
			}

			return new GuestHouseDto
			{
				Id = guestHouse.Id,
				Name = guestHouse.Name,
				Description = guestHouse.Name
			};
		}
	}
}
