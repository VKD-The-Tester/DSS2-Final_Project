using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public ActionResult<User> register(UserDto request)
        {
            return Ok();
        }
        
        [HttpPost("login")]
        public IActionResult login(UserDto request)
        {
            return Ok();
        }
    }
}
