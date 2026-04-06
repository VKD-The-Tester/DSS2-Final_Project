using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class LoginRequestDto
    {
        [EmailAddress(ErrorMessage = "The email format is invalid."), JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }
}
