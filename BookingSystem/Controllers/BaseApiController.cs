using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookingSystem.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]

    public class BaseApiController : ControllerBase
    {
        [NonAction]
        public string GetCurrentUser() =>
             HttpContext.User.FindFirstValue(ClaimTypes.Name);
    }
}
