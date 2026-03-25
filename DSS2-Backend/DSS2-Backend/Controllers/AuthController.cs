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
        private readonly ApplicationDbContext _context;

        public AuthController(IPasswordService passwordService, ApplicationDbContext context)
        {
            this._passwordService = passwordService;
            this._context = context;
        }

        [HttpPost("register")]
        public ActionResult<string> register(RegisterRequestDto request)
        {
            /* userExists = _context.Users.FirstOrDefault(u => u.Email == request.Email);
             * if(userExists) { return Conflict("A user already exists with this email.); }
             */
            
            // A new user will have to get created through the register method.
            User user = new User();

            // The response data transfer object should also be created and returned.
            RegisterResponseDto response = new RegisterResponseDto
            {
                Id = user.Id,
                Email = request.Email,
                DisplayName = request.DisplayName,
            };

            // The response will have to get serialized, to satisfy the requirements.
            string serializedResponse = JsonSerializer.Serialize(response);

            // A 201 status code should get returned with the specified response data transfer object in JSON form.
            return Created("", serializedResponse);
        }
        
        [HttpPost("login")]
        public IActionResult login(RegisterRequestDto request)
        {
            return Ok();
        }
    }
}
