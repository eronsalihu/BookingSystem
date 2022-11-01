using BookingSystem.Dtos;
using BookingSystem.Entities;

namespace BookingSystem.Interfaces
{
    public interface IGuestHouseService
    {
        Task<List<GuestHouseDto>> GetAllGuestHousesAsync(string? checkIn, string? checkOut, int numberOfBeds);
        GuestHouseDto GetGuestHousesById(int id);
        Task<GuestHouseDto> AddGuestHouseAsync(GuestHouse guestHouse);
        Task<GuestHouseDto> UpdateGuestHouseAsync(GuestHouse guestHouse);
        void DeleteGuestHouseAsync(int id);
        Task<List<GuestHouseDto>> GetTopFiveBookedGuestHoues();
    }
}
