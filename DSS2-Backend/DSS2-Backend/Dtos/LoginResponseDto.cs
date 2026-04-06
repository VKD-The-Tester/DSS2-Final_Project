using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class LoginResponseDto
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = null!;

        [JsonPropertyName("tokenType")]
        public string TokenType { get; set; } = null!;

        [JsonPropertyName("expiresInSeconds")]
        public int ExpiresInSeconds { get; set; }

        [JsonPropertyName("user")]
        public Dictionary<string, string> User { get; set; } = null!;
    }
}
