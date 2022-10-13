using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    public class RoomController : BaseApiController
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("room")]
        public async Task<IActionResult> GetRooms(int id)
        {
            return Ok(_roomService.GetRoomsByGuestHouseId(id).Result);
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
                    Days = roomDto.Days,
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
        public IActionResult UpdateRoom(int id, [FromBody] RoomDto roomDto)
        {
            var room = new Room
            {
                Id = id,
                Name = roomDto.Name,
                Image = roomDto.Image,
                Description = roomDto.Description,
                Price = roomDto.Price,
                Days = roomDto.Days,
                NumberOfBeds = roomDto.NumberOfBeds,
                CreatedBy = GetCurrentUser(),
                GuestHouseId = roomDto.GuestHouseId,
                Amenities = roomDto.Amenities.Select(e => new RoomAmenity
                {
                    RoomId = id,
                    Amenities = e
                }).ToList() ?? null,
            };
            var updatedRoom = _roomService.UpdateRoomAsync(room);
            if (updatedRoom.Result == null)
                return BadRequest(new ApiException(404, "Room not found"));

            return Ok(updatedRoom.Result);
        }

        [HttpPost("uploadImage")]
        public async Task<IActionResult> UploadImage(int id, [FromBody] byte[] image)
        {
            var roomImage = await _roomService.AddImage(id, image);

            if (roomImage == null) return BadRequest(new ApiException(404, "Couldn't add image"));

            return Ok(roomImage);
        }

        [HttpDelete("room")]
        public void DeleteRoom(int id) =>
             _roomService.DeleteRoomAsync(id);
    }
}
