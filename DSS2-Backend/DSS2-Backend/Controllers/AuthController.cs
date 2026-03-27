using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public AuthController(IPasswordService passwordService, ITokenService tokenService, ApplicationDbContext context)
        {
            this._passwordService = passwordService;
            this._tokenService = tokenService;
            this._context = context;
        }

        [HttpPost("register")]
        public ActionResult<string> Register(RegisterRequestDto request)
        {
            return Created();
        }
        
        [HttpPost("login")]
        public IActionResult Login(RegisterRequestDto request)
        {
            return Ok();
        }
    }
}
