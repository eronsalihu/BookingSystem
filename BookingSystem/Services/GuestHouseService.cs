using BookingSystem.Data;
using BookingSystem.Data.Identity;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<GuestHouse> AddGuestHouseAsync(GuestHouse guestHouse)
        {
            await _context.GuestHouses.AddAsync(guestHouse);
            await _context.SaveChangesAsync();
            return guestHouse;
        }

        public async Task<List<GuestHouse>> GetGuestHousesAsync(string? id)
        {
            if (id != null)
            {
                var user = _identityContext.Users.Where(e => e.Id == id).FirstOrDefault();
                if (user.Role == Role.GuestHouse)
                {
                    return await _context.GuestHouses.Include(e => e.Rooms).ThenInclude(e => e.Amenities).Where(e => e.CreatedBy == id).ToListAsync();
                }
            }
            return await _context.GuestHouses.Include(e => e.Rooms).ThenInclude(e => e.Amenities).ToListAsync();

        }

        public async Task<GuestHouse> UpdateGuestHouseAsync(GuestHouse guestHouse)
        {
            if (_context.GuestHouses.SingleOrDefault(e => e.Id == guestHouse.Id) == null) return null;
             
            _context.GuestHouses.Update(guestHouse);
            await _context.SaveChangesAsync();
            return guestHouse;
        }

        public async void DeleteGuestHouseAsync(int id)
        { 
            var guestHouse = _context.GuestHouses.SingleOrDefault(e => e.Id == id);
            _context.GuestHouses.Remove(guestHouse);
            await _context.SaveChangesAsync();

        }
    }
}
