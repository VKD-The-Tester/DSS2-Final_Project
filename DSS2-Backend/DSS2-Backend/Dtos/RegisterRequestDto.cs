using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class RegisterRequestDto
    {
        [Required, EmailAddress(ErrorMessage = "The email format is invalid."), MaxLength(254), JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("password"), Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{6,128}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and should be at least six characters long.")]
        public string Password { get; set; } = null!;

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; } = "";
    }
}
