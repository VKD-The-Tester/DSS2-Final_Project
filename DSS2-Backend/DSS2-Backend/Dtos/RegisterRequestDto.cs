namespace DSS2_Backend.Dtos
{
    public class RegisterRequestDto
    {
        public required string Email { get; set; } = null!;

        public required string Password { get; set; } = null!;

        public string? DisplayName { get; set; } = string.Empty;
    }
}
