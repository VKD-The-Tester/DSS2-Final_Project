using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPasswordService _passwordService;
        private readonly ApplicationDbContext _context;

        public AuthController(IPasswordService passwordService, ApplicationDbContext context)
        {
            this._passwordService = passwordService;
            this._context = context;
        }

        [HttpPost("register")]
        public ActionResult<RegisterResponseDto> register(RegisterRequestDto request)
        {
            // A new user will have to get created through the register method.
            User user = new User();

            // A 201 status code should return with the specified response data transfer object.
            return Created();
        }
        
        [HttpPost("login")]
        public IActionResult login(RegisterRequestDto request)
        {
            return Ok();
        }
    }
}
