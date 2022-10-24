using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    [Authorize]
    public class BookingsController : BaseApiController
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IEnumerable<BookDto>> GetBookings() =>
            await _bookingService.GetBookingsAsync();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookingById(int id) =>
            Ok(await _bookingService.GetBookingByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> AddBooking(BookDto bookDto)
        {
            var book = new Book
            {
                BookFrom = bookDto.BookFrom,
                BookTo = bookDto.BookTo,
                GuestHouseId = bookDto.GuestHouseId,
                CreatedBy = GetCurrentUser()
            };
            var addedBook = await _bookingService.AddBookAsync(book);
            if (addedBook == null)
                return BadRequest(new ApiException(404, "Couldn't book guest house!"));
            return Ok(addedBook);
        }

        [HttpGet("GuestHouse/{id}")]
        public async Task<IEnumerable<BookDto>> GetBookedDays(int id) =>
           await _bookingService.GetBookedGuestHousePerDays(id);
    }
}
