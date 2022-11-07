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

        [HttpGet("GuestHouse/{id}")]
        public async Task<IActionResult> GetRooms(int id)
        {
            return Ok(_roomService.GetRoomsByGuestHouseId(id).Result);
        }

        [HttpGet("{id}")]
        public async Task<RoomDto> GetRoomById(int id)
        {
            return _roomService.GetRoomById(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRoom([FromBody] RoomDto roomDto)
        {
            var room = new Room
            {
                Name = roomDto.Name,
                Description = roomDto.Description,
                Image = roomDto.Image,
                Price = roomDto.Price,
                NumberOfBeds = roomDto.NumberOfBeds,
                CreatedBy = GetCurrentUser(),
                GuestHouseId = roomDto.GuestHouseId,
                Amenities = roomDto.Amenities.Select(e => new RoomAmenity
                {
                    Amenities = e
                }).ToList() ?? null,

            };

            return Ok(await _roomService.AddRoomsAsync(room));
        }
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, [FromBody] RoomDto roomDto)
        {
            var room = new Room
            {
                Id = id,
                Name = roomDto.Name,
                Image = roomDto.Image,
                Description = roomDto.Description,
                Price = roomDto.Price,
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
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteRoom(int id) =>
                     _roomService.DeleteRoomAsync(id);
        [Authorize]
        [HttpPost("book")]
        public async Task<IActionResult> BookRoom([FromBody] BookDto bookDto)
        {
            var book = new Book
            {
                RoomId = bookDto.Id,
                BookFrom = bookDto.BookFrom,
                BookTo = bookDto.BookTo,
                CreatedBy = GetCurrentUser(),
            };

            return Ok(await _roomService.BookRoomAsync(book));
        }
    }
}
