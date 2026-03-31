using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // TODO: Fix the VerifiedPassword method.
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IPasswordService passwordService, ITokenService tokenService, IConfiguration configuration, ApplicationDbContext context)
        {
            this._passwordService = passwordService;
            this._tokenService = tokenService;
            this._configuration = configuration;
            this._context = context;
        }

        [HttpPost("register")]
        public ActionResult Register(RegisterRequestDto request)
        {
            if (_context.Users.FirstOrDefault(u => u.Email == request.Email) != null) 
            {
                return Conflict("A user is already registered with this email.");
            }

            var hashedPassword = _passwordService.HashPassword(request);

            User newUser = new User { 
                Email =  request.Email,
                PasswordHash = hashedPassword,
                DisplayName = request.DisplayName,
                Roles = Roles.User,
            };

            _context.Users.Add(newUser);

            _context.SaveChanges();

            RegisterResponseDto response = new RegisterResponseDto { 
                Email = request.Email, 
                DisplayName = request.DisplayName,
                Id = newUser.Id
            };
           
            var serializedResponse = JsonSerializer.Serialize(response);

            return Created("", serializedResponse);
        }
        
        [HttpPost("login")]
        public ActionResult Login(LoginRequestDto request)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == request.Email);

            if (existingUser == null)
            {
                return BadRequest("There is no account registered under this email address.");
            }

            if(_passwordService.VerifyPassword(existingUser, request) == false)
            {
                return Unauthorized("The provided password is incorrect.");
            }

            var token = _tokenService.CreateAccessToken(existingUser);

            var tokenDetails = _configuration.GetSection("Jwt");

            Dictionary<string, string> userDetails = new Dictionary<string, string>();

            if (existingUser.DisplayName != null) 
            {
                userDetails.Add("id", existingUser.Id.ToString());
                userDetails.Add("email", existingUser.Email);
                userDetails.Add("displayName", existingUser.DisplayName);
            }

            else
            {
                userDetails.Add("id", existingUser.Id.ToString());
                userDetails.Add("email", existingUser.Email);
            }

            LoginResponseDto response = new LoginResponseDto
            {
                AccessToken = token,
                TokenType = "Bearer",
                ExpiresInSeconds = int.Parse(tokenDetails["ExpiresMinutes"]!) * 60,
                User = userDetails
            };

            var serializedResponse = JsonSerializer.Serialize(response);

            return Ok(serializedResponse);
        }
    }
}
