using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IGuestHouseService
    {
        Task<List<GuestHouse>> GetGuestHousesAsync(string? id);
        Task<GuestHouse> AddGuestHouseAsync(GuestHouse guestHouse);
        Task<GuestHouse> UpdateGuestHouseAsync(GuestHouse guestHouse);
        void DeleteGuestHouseAsync(int id);
    }
}
