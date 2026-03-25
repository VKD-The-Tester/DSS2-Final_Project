namespace DSS2_Backend.Dtos
{
    public class RegisterResponseDto
    {
        public Guid Id { get; set; }

        public required string Email { get; set; }

        public string? DisplayName { get; set; }
    }
}
