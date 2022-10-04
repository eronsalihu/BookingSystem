using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class BookController : BaseApiController
    {
        private readonly IBookingService _bookingService;

        public BookController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("book")]
        public async Task<IActionResult> AddBooking(BookDto bookDto)
        {
            var book = new Book
            {
                GuestHouseId = bookDto.GuestHouseId,
                BookFrom = bookDto.BookFrom,
                BookTo = bookDto.BookTo,
            };
            if ((await _bookingService.AddBookAsync(book) == null)) return BadRequest(new ApiException(404, "Couldn't book guest house!"));
            return Ok(await _bookingService.AddBookAsync(book));
        }

        [HttpGet("bookings/{id}")]
        public async Task<List<Book>> GetBookedDays(int id) =>
           await _bookingService.GetBookedGuestHousePerDays(id);
    }
}
