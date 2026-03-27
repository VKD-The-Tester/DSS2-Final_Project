using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.Dtos
{
    public class LoginRequestDto
    {
        [EmailAddress]
        public required string Email { get; set; } = null!;

        [DataType(DataType.Password)]
        public required string Password { get; set; } = null!;
    }
}
