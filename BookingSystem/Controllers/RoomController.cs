using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class RoomController : BaseApiController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpPut("room/{id}")]
        public async Task<ActionResult> UpdateGuestHouse(int id, RoomDto roomDto)
        {
            var room = new Room
            {
                Id = id,
                Name = roomDto.Name,
                Description = roomDto.Description,
                Image = roomDto.Image,
                Price = roomDto.Price,
                Day = roomDto.Day,
                NumberOfBeds = roomDto.NumberOfBeds,
                CreatedBy = GetCurrentUser(),
                GuestHouseId = roomDto.GuestHouseId,
                Amenities = (ICollection<RoomAmenity>)roomDto.Amenities.ToList() ?? null,
            };

            return Ok();
        }

        [HttpDelete("{id}")]
        public void DeleteRoom(int id) =>
             _roomService.DeleteRoomAsync(id);


    }
}
