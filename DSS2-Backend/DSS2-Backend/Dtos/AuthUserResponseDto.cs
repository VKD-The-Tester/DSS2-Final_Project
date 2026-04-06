using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class AuthUserResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("displayName")]
        public string? DisplayName { get; set; }
    }
}
