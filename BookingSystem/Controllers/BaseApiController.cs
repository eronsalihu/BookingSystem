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
        [NonAction]
        public string GetCurrentUser() =>
             HttpContext.User.FindFirstValue(ClaimTypes.Name);

        [NonAction]
        public byte[] ConvertToBase64(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    return ms.ToArray(); 
                     
                }
            }
            return null;
        }
    }
}
