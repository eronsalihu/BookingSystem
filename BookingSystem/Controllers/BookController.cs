using BookingSystem.Dtos;
using BookingSystem.Interfaces;
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

        [HttpGet("GuestHouse/{id}")]
        public async Task<List<BookDto>> GetBookedDays(int id) =>
           await _bookingService.GetBookedGuestHouesPerDays(id);
    }
}
