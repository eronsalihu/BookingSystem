using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class GuestHouseController : BaseApiController
    {
        private IGuestHouseService _guestHouseService;

        public GuestHouseController(IGuestHouseService guestHouseService)
        {
            _guestHouseService = guestHouseService;
        }

        [HttpGet("guest-house")]
        public async Task<ActionResult<List<GuestHouseDto>>> GetGuestHouses() =>
            await _guestHouseService.GetGuestHousesAsync(GetCurrentUser());

        [HttpPost("guest-house")]
        public async Task<IActionResult> AddGuestHouse(GuestHouseDto guestHouseDto)
        {
            var guestHouse = new GuestHouse
            {
                Name = guestHouseDto.Name,
                Description = guestHouseDto.Description,
                CreatedBy = GetCurrentUser(),
            };

            return Ok(await _guestHouseService.AddGuestHouseAsync(guestHouse));
        }

        [HttpPut("guest-house")]
        public async Task<IActionResult> UpdateGuestHouse(int id, GuestHouseDto guestHouseDto)
        {
            var guestHouse = new GuestHouse
            {
                Id = id,
                Name = guestHouseDto.Name,
                Description = guestHouseDto.Description,
                CreatedBy = GetCurrentUser(),
            };

            var ghUpdate = await _guestHouseService.UpdateGuestHouseAsync(guestHouse);

            if (ghUpdate == null)
                return NotFound(new ApiResponse(404, "Guest House not found"));

            return Ok(ghUpdate);
        }

        [HttpDelete("guest-house")]
        public void DeleteGuestHouse(int id) =>
              _guestHouseService.DeleteGuestHouseAsync(id);
    }
}
