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

        public async Task<List<RoomDto>> AddRoomsAsync(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                await _context.Rooms.AddAsync(room);
            }

            await _context.SaveChangesAsync();

            return rooms.Select(e => new RoomDto
            {
                Id = e.Id,
                Name = e.Name,
                Image = e.Image,
                Description = e.Description,
                Price = e.Price,
                Days = e.Days,
                NumberOfBeds = e.NumberOfBeds,
                GuestHouseId = e.GuestHouseId,
                Amenities = e.Amenities.Select(a => a.Amenities).ToList()
            }).ToList();

        }

        public async Task<RoomDto> UpdateRoomAsync(Room room)
        {
            if (await _context.Rooms.AsNoTracking().SingleOrDefaultAsync(e => e.Id == room.Id) == null)
            {
                throw new KeyNotFoundException($"No room found with id: {room.Id}");
            }

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Description = room.Description,
                Image = room.Image,
                Price = room.Price,
                Days = room.Days,
                NumberOfBeds = room.NumberOfBeds,
                GuestHouseId = room.GuestHouseId,
                Amenities = room.Amenities.Select(e => e.Amenities).ToList(),
            };
        }

        public async Task<RoomDto> AddImage(int id, byte[] encodedImage)
        {
            var room = _context.Rooms.Where(e => e.Id == id).FirstOrDefault();

            if (room == null)
            {
                throw new KeyNotFoundException($"No room found with id: {id}");
            }

            room.Image = encodedImage;
            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();

            return new RoomDto
            {
                Id = id,
                Name = room.Name,
                Description = room.Description,
                Price = room.Price,
                Days = room.Days,
                Image = room.Image,
                NumberOfBeds = room.NumberOfBeds,
                GuestHouseId = room.GuestHouseId,
                Amenities = room.Amenities.Select(e => e.Amenities).ToList(),
            };
        }

        public async Task<List<RoomDto>> GetRoomsByGuestHouseId(int guestHouseId)
        {
            return await _context.Rooms.Where(e => e.GuestHouseId == guestHouseId).Select(e => new RoomDto
            {
                Id = e.Id,
                Name = e.Name,
                Image = e.Image,
                Description = e.Description,
                Price = e.Price,
                Days = e.Days,
                NumberOfBeds = e.NumberOfBeds,
                GuestHouseId = e.GuestHouseId,
                Amenities = e.Amenities.Select(a => a.Amenities).ToList()

            }).ToListAsync();
        }

        public async void DeleteRoomAsync(int id)
        {
            var room = _context.Rooms.SingleOrDefault(e => e.Id == id);

            if (room == null)
            {
                throw new KeyNotFoundException($"No room found with id: {id}");
            }
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
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
                Days = room.Days,
                Image = room.Image,
                NumberOfBeds = room.NumberOfBeds,
                GuestHouseId = room.GuestHouseId,
                Amenities = _context.RoomAmenities.Where(e=>e.RoomId == room.Id).Select(e=>e.Amenities).ToList()?? null,
            };
        }
    }
}
