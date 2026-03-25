using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public IActionResult register()
        {
            return Ok();
        }
        
        [HttpPost("login")]
        public IActionResult login()
        {
            return Ok();
        }
    }
}
