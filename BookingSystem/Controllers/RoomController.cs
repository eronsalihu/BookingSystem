using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using System;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpPost("room")]
        public async Task<IActionResult> AddRoom([FromBody] List<RoomDto> roomDtos)
        {
            var roomList = new List<Room>();
            foreach (var roomDto in roomDtos)
            {
                var room = new Room
                {
                    Name = roomDto.Name,
                    Description = roomDto.Description,
                    Price = roomDto.Price,
                    Day = roomDto.Day,
                    NumberOfBeds = roomDto.NumberOfBeds,
                    CreatedBy = GetCurrentUser(),
                    GuestHouseId = roomDto.GuestHouseId,
                    Amenities = roomDto.Amenities.Select(e => new RoomAmenity
                    { 
                        Amenities = e
                    }).ToList() ?? null,

                };
                roomList.Add(room);
            }

            return Ok(await _roomService.AddRoomsAsync(roomList));
        }

        [HttpPut("room")]
        public async Task<IActionResult> UpdateGuestHouse(int id, [FromBody] RoomDto roomDto)
        {
            var room = new Room
            {
                Id = id,
                Name = roomDto.Name,
                Description = roomDto.Description,
                Price = roomDto.Price,
                Day = roomDto.Day,
                NumberOfBeds = roomDto.NumberOfBeds,
                CreatedBy = GetCurrentUser(),
                GuestHouseId = roomDto.GuestHouseId,
                Amenities = roomDto.Amenities.Select(e => new RoomAmenity
                {
                    Amenities = e
                }).ToList() ?? null,
            };
            if (_roomService.UpdateRoomAsync(room) == null) return BadRequest(new ApiException(404, "Room not found"));

            return Ok(_roomService.UpdateRoomAsync(room));
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(int id, [FromForm] IFormFile file) =>
             Ok(await _roomService.AddImage(id, ConvertToBase64(file)));

        [HttpDelete("{id}")]
        public void DeleteRoom(int id) =>
             _roomService.DeleteRoomAsync(id);
    }
}
