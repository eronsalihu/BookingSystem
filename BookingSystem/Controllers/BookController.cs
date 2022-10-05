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
        public async Task<IActionResult> AddBooking(int id, BookDto bookDto)
        {
            var book = new Book
            {
                Id = id,
                BookFrom = bookDto.BookFrom,
                BookTo = bookDto.BookTo,
                CreatedBy = GetCurrentUser()
            };
            var addedBook = await _bookingService.AddBookAsync(book);
            if (addedBook == null)
                return BadRequest(new ApiException(404, "Couldn't book guest house!"));
            return Ok(addedBook);
        }

        [HttpGet("bookings")]
        public async Task<List<BookDto>> GetBookedDays(int id) =>
           await _bookingService.GetBookedGuestHousePerDays(id);
    }
}
