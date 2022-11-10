using BookingSystem.Dtos;
using BookingSystem.Interfaces;
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

        [HttpGet("{id}")]
        public async Task<List<BookDto>> GetBookedDays(int id) =>
           await _bookingService.GetBookedGuestHouesPerDays(id);

        [HttpGet("User/{id}")]
        public async Task<List<BookDto>> GetBookingsByUserId(string id) =>
           await _bookingService.GetBookingsByUserId(id);
    }
}
