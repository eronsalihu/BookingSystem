using BookingSystem.Dtos;
using BookingSystem.Entities;
using BookingSystem.Interfaces;
using BookingSystem.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Controllers
{
    public class AuthenticationController : BaseApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;

        public AuthenticationController(UserManager<User> userManager, SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }


        [HttpGet("email-exists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email) =>
            await _userManager.FindByEmailAsync(email) != null;

        [HttpGet("username")]
        public async Task<ActionResult<bool>> CheckUsernameExistsAsync([FromQuery] string username) =>
            await _userManager.FindByNameAsync(username) != null;

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            return new UserDto
            {
                Username = user.UserName,
                Role = user.Role,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            };

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is already in use." } });
            }
            if (CheckUsernameExistsAsync(registerDto.Username).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Username is not avaliable." } });
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                UserName = registerDto.Username,
                Role = Role.User
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            if (await _roleManager.RoleExistsAsync(Role.User))
                await _userManager.AddToRoleAsync(user, Role.User);

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            };
        }


        /*[HttpPost("register-guesthouse")]
        public async Task<ActionResult<UserDto>> RegisterGuestHouse(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is already in use" } });
            }
            if (CheckUsernameExistsAsync(registerDto.Username).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Username is not avaliable." } });
            }

            var user = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Username,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                Role = Role.GuestHouse
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            if (await _roleManager.RoleExistsAsync(Role.GuestHouse))
                await _userManager.AddToRoleAsync(user, Role.GuestHouse);

            return new UserDto
            {
                Username = user.UserName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
            };
        }*/
    }
}
