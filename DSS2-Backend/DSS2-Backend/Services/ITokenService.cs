using DSS2_Backend.Models;

namespace DSS2_Backend.Services
{
    public interface ITokenService
    {
        string CreateAccessToken(User user);
    }
}
