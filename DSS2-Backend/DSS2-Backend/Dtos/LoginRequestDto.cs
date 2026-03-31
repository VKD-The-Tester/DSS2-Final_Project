using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.Dtos
{
    public class LoginRequestDto
    {
        [Required, EmailAddress(ErrorMessage = "The email format is invalid.")]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
