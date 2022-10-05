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

        public async Task<object> AddImage(int id, byte[] encodedImage)
        {
            var room = _context.Rooms.Where(e => e.Id == id).FirstOrDefault();
            if (room == null) return null;
            room.Image = encodedImage;
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            return new
            {
                Id = id,
                Name = room.Name,
                Description = room.Description,
                Price = room.Price,
                Days = room.Days,
                Image = room.Image,
                NumberOfBeds = room.NumberOfBeds,
                GuestHouse = _context.GuestHouses.Where(e => e.Id == room.GuestHouseId).Select(e => e.Name).FirstOrDefault(),
                Amenities = room.Amenities.Select(e => e.Amenities.ToString()).ToList()
            };
        }

        public async Task<List<object>> AddRoomsAsync(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                await _context.Rooms.AddAsync(room);
            }

            await _context.SaveChangesAsync();

            return rooms.Select(e => new
            {
                Id = e.Id,
                Name = e.Name,
                Description = e.Description,
                Price = e.Price,
                Days = e.Days,
                NumberOfBeds = e.NumberOfBeds,
                GuestHouse = _context.GuestHouses.Where(r => r.Id == e.GuestHouseId).Select(r => r.Name).FirstOrDefault(),
                Amenities = _context.RoomAmenities.Where(r => r.RoomId == e.Id).Select(r => r.Amenities.ToString()).ToList()
            }).ToList<object>();

        }

        public async void DeleteRoomAsync(int id)
        {
            var room = _context.Rooms.SingleOrDefault(e => e.Id == id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<object> UpdateRoomAsync(Room room)
        {
            if (await _context.Rooms.AsNoTracking().SingleOrDefaultAsync(e => e.Id == room.Id) == null) return null;
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return new
            {
                Name = room.Name,
                Description = room.Description,
                Price = room.Price,
                Days = room.Days,
                NumberOfBeds = room.NumberOfBeds,
                GuestHouse = _context.GuestHouses.Where(e => e.Id == room.GuestHouseId).Select(e => e.Name).FirstOrDefault(),
                Amenities = _context.RoomAmenities.Where(e => e.RoomId == room.Id).Select(e => e.Amenities.ToString()).ToList()
            };
        }

    }
}
