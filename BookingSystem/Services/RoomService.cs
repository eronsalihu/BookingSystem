using BookingSystem.Data;
using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Text;

namespace BookingSystem.Services
{
    public class RoomService : IRoomService
    {
        private readonly BookingContext _context;

        public RoomService(BookingContext context)
        {
            _context = context;
        }

        public async Task<Room> AddImage(int id, string encodedImage)
        {
            var room = _context.Rooms.Where(e => e.Id == id).FirstOrDefault();
            if (room == null) return null;
            room.Image = Encoding.UTF8.GetBytes(encodedImage);
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }

        public async Task<List<Room>> AddRoomsAsync(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                await _context.Rooms.AddAsync(room);
            }

            await _context.SaveChangesAsync();

            return rooms.Select(e => new Room
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                Day = e.Day,
                NumberOfBeds = e.NumberOfBeds,
                GuestHouseId = e.GuestHouseId,
                Amenities = e.Amenities.Select(e => new RoomAmenity
                {
                    Amenities = e.Amenities
                }).ToList() ?? null,
            }).ToList();
        }

        public async void DeleteRoomAsync(int id)
        {
            var room = _context.Rooms.SingleOrDefault(e => e.Id == id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }
        public async Task<Room> UpdateRoomAsync(Room room)
        {
            if (await _context.GuestHouses.AsNoTracking().SingleOrDefaultAsync(e => e.Id == room.Id) == null) return null;
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }
         
    }
}
