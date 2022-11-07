using BookingSystem.Entities;
using BookingSystem.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{

    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUsers() =>
            Ok(await _userService.GetUsersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(string id) =>
            Ok(await _userService.GetUserByIdAsync(id));

        [HttpPut("{id}")]
        public async Task<ActionResult<User>> UpdateUser(string id, [FromBody] User user)
        {
            user.Id = id;
            return Ok(_userService.UpdateUser(user));
        }
    }
}
