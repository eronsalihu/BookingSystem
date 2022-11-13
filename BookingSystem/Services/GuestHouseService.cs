﻿using BookingSystem.Data;
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

        public GuestHouseService(BookingContext context, IdentityContext identityContext)
        {
            _context = context;
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

        public async Task<List<GuestHouseDto>> GetAllGuestHousesAsync(DateTime? checkIn, DateTime? checkOut, int? numberOfBeds)
        {
            return await (from gh in _context.GuestHouses
                          join r in _context.Rooms on gh.Id equals r.GuestHouseId into roomLeft
                          from rooms in roomLeft.DefaultIfEmpty()
                          join b in _context.Bookings on rooms.Id equals b.RoomId into bookingLeft
                          from bookings in bookingLeft.DefaultIfEmpty()
                          where
                          (checkIn != null ? bookings.BookFrom.Date <= checkIn.Value.Date : true)
                          &&
                          (checkOut != null ? bookings.BookTo.Date >= checkOut.Value.Date : true)
                          &&
                          (numberOfBeds != null ? rooms.NumberOfBeds == numberOfBeds : true)
                          select new GuestHouseDto
                          {
                              Id = gh.Id,
                              Name = gh.Name,
                              Description = gh.Description
                          }).Distinct().ToListAsync();
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
                Description = guestHouse.Description
            };
        }

        public async Task<List<GuestHouseDto>> GetTopFiveBookedGuestHoues()
        {
            var guestHouses = await (from gh in _context.GuestHouses
                                     join r in _context.Rooms on gh.Id equals r.GuestHouseId
                                     join b in _context.Bookings on r.Id equals b.RoomId
                                     group gh by gh.Id into g
                                     select new
                                     {
                                         Id = g.Key,
                                         Name = g.Select(e => e.Name).FirstOrDefault(),
                                         Description = g.Select(e => e.Description).FirstOrDefault(),
                                         Count = g.Count()
                                     }).OrderByDescending(t => t.Count).Take(5).ToListAsync();

            return guestHouses.Select(e => new GuestHouseDto
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description
            }).ToList();


        }
    }
}
