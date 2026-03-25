using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.ViewModels;

namespace DSS2_Backend.Services
{
    public interface IPasswordService
    {
        string HashPassword(User user, UserDto request);
    }
}
