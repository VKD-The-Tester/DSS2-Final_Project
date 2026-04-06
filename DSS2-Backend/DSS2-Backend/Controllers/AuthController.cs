using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
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
        public async Task<ActionResult<AuthUserResponseDto>> Register(RegisterRequestDto request)
        {
            if (await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email) != null) 
            {
                return Conflict(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/409",
                    Title = "Validation Failed",
                    Status = 409
                });
            }

            var hashedPassword = _passwordService.HashPassword(request);

            User newUser = new User { 
                Email =  request.Email,
                PasswordHash = hashedPassword,
                DisplayName = request.DisplayName
            };

            await _context.Users.AddAsync(newUser);

            await _context.SaveChangesAsync();

            AuthUserResponseDto response = new AuthUserResponseDto { 
                Email = request.Email, 
                DisplayName = request.DisplayName,
                Id = newUser.Id
            };
           
            return Created("", response);
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto request)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (existingUser == null)
            {
                return BadRequest(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/400",
                    Title = "Validation Failed",
                    Status = 400
                });
            }

            if(_passwordService.VerifyPassword(existingUser, request) == false)
            {
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "Validation Failed",
                    Status = 401
                });
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

            return Ok(response);
        }
    }
}
