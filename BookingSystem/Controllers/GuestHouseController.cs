using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    public class GuestHouseController : BaseApiController
    {
        private IGuestHouseService _guestHouseService;

        public GuestHouseController(IGuestHouseService guestHouseService)
        {
            _guestHouseService = guestHouseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GuestHouseDto>>> GetGuestHouses(DateTime? checkIn, DateTime? checkOut, int? numberOfBeds)
        {
            return await _guestHouseService.GetAllGuestHousesAsync(checkIn, checkOut, numberOfBeds);
        }

        [HttpGet("{id}")]
        public ActionResult<GuestHouseDto> GetGuestHouseById(int id)
        {
            return _guestHouseService.GetGuestHousesById(id);
        }

        [Authorize]
        [HttpPost]
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
        [Authorize]
        [HttpPut("{id}")]
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
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteGuestHouse(int id) =>
                      _guestHouseService.DeleteGuestHouseAsync(id);

        [HttpGet("top-five")]
        public async Task<IActionResult> TopFive()
            => Ok(await _guestHouseService.GetTopFiveBookedGuestHoues());
    }
}
