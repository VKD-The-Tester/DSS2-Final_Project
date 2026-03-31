using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace DSS2_Backend.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(RegisterRequestDto request)
        {
            var hashedPassword = new PasswordHasher<RegisterRequestDto>().HashPassword(request, request.Password);

            return hashedPassword;
        }

        public bool VerifyPassword(User user, LoginRequestDto request)
        {
            if (new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password) == PasswordVerificationResult.Failed)
            {
                return false;
            }
            
           return true;
        }
    }
}
