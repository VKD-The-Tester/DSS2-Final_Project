using DSS2_Backend.Dtos;
using DSS2_Backend.Models;

namespace DSS2_Backend.Services
{
    public interface IPasswordService
    {
        string HashPassword(RegisterRequestDto request);

        bool VerifyPassword(User user, LoginRequestDto request);
    }
}
