using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class RegisterRequestDto
    {
        [EmailAddress(ErrorMessage = "The email format is invalid."), MaxLength(254), JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and should be at least eight characters long.")]
        [JsonPropertyName("password"), Length(6, 128)]
        public string Password { get; set; } = null!;

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }
}
