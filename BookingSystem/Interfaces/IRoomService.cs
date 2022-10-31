using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IRoomService
    {
        Task<List<RoomDto>> GetRoomsByGuestHouseId(int guestHouseId);
        RoomDto GetRoomById(int id);
        Task<RoomDto> AddRoomsAsync(Room rooms);
        Task<RoomDto> UpdateRoomAsync(Room room);
        Task<BookDto> BookRoomAsync(Book book);
        void DeleteRoomAsync(int id);
    }
}
