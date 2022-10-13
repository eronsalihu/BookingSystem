using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetRoomsByGuestHouseId(int guestHouseId);
        RoomDto GetRoomById(int id);
        Task<List<RoomDto>> AddRoomsAsync(List<Room> rooms);
        Task<RoomDto> UpdateRoomAsync(Room room);
        Task<RoomDto> AddImage(int id, byte[] encodedImage);
        void DeleteRoomAsync(int id);
    }
}
