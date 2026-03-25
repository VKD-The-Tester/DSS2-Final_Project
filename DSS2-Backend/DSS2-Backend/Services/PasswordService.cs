using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace DSS2_Backend.Services
{
    public class PasswordService : IPasswordService
    {
        // A random 64-bit salt.
        private const string Salt = "V8fmyM/+36CprFeSXKXXTTVdnhKnqtTC7ERred3A3XmHh3J3hCZBD2Rn9n1U8ymy";

        public string HashPassword(User user, RegisterRequestDto request)
        {
            string finalPassword = $"{Salt}{request.Password}{request.Email}";

            var hashedPassword = new PasswordHasher<User>().HashPassword(user, finalPassword);

            return hashedPassword;
        }
    }
}
