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

        public AuthController(IPasswordService passwordService)
        {
            this._passwordService = passwordService;
        }

        [HttpPost("register")]
        public ActionResult<RegisterResponseDto> register(RegisterRequestDto request)
        {
            return Ok();
        }
        
        [HttpPost("login")]
        public IActionResult login(RegisterRequestDto request)
        {
            return Ok();
        }
    }
}
