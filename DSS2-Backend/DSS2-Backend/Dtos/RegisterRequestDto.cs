using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.Dtos
{
    public class RegisterRequestDto
    {
        [Required, EmailAddress(ErrorMessage = "The email format is invalid.")]
        public required string Email { get; set; }

        [Required, RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d).{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and should be at least eight characters long.")]
        public required string Password { get; set; }

        public string? DisplayName { get; set; } = null;
    }
}
