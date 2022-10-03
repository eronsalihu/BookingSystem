using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IRoomService
    {
        Task<List<Room>> AddRoomsAsync(List<Room> rooms);
        Task<Room> UpdateRoomAsync(Room room);
        void DeleteRoomAsync(int id);
    }
}
