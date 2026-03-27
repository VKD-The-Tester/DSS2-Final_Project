using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.Dtos
{
    public class LoginRequestDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
