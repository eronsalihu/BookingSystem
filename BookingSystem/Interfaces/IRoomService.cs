using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IRoomService
    {
        Task<List<object>> AddRoomsAsync(List<Room> rooms);
        Task<object> UpdateRoomAsync(Room room);
        Task<object> AddImage(int id, byte[] encodedImage);
        void DeleteRoomAsync(int id);
    }
}
