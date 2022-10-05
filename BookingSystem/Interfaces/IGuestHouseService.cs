using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IGuestHouseService
    {
        Task<List<GuestHouseDto>> GetGuestHousesAsync(string? id);
        Task<GuestHouseDto> AddGuestHouseAsync(GuestHouse guestHouse);
        Task<GuestHouseDto> UpdateGuestHouseAsync(GuestHouse guestHouse);
        void DeleteGuestHouseAsync(int id);
    }
}
