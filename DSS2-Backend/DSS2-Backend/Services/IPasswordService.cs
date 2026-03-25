using DSS2_Backend.ViewModels;

namespace DSS2_Backend.Services
{
    public interface IPasswordService
    {
        string HashPassword(UserVM user);
    }
}
