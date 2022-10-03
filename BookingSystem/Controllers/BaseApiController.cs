using BookingSystem.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 

    public class BaseApiController : ControllerBase
    {
        public string GetCurrentUser() =>
             HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}
