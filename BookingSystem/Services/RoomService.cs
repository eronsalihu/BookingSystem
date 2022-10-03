using BookingSystem.Data;
using BookingSystem.Entities;
using BookingSystem.Interfaces;

namespace BookingSystem.Services
{
    public class RoomService : IRoomService
    {
        private readonly BookingContext _context;

        public RoomService(BookingContext context)
        {
            _context = context;
        }

        public async Task<List<Room>> AddRoomsAsync(List<Room> rooms)
        {
            foreach (var room in rooms)
            {
                await _context.Rooms.AddAsync(room);
            }
            await _context.SaveChangesAsync();
            return rooms.ToList();
        }

        public async void DeleteRoomAsync(int id)
        {
            var room = _context.Rooms.SingleOrDefault(e => e.Id == id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            if (_context.Rooms.SingleOrDefault(e => e.Id == room.Id) == null) return null;

            _context.Rooms.Update(room);
            await _context.SaveChangesAsync();
            return room;
        }
    }
}
