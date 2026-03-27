using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.Dtos
{
    public class RegisterRequestDto
    {
        [Required, EmailAddress(ErrorMessage = "The email format is invalid.")]
        public required string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }

        public string? DisplayName { get; set; } = null;
    }
}
