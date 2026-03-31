namespace DSS2_Backend.Dtos
{
    public class LoginResponseDto
    {
        public required string AccessToken {  get; set; }

        public required string TokenType { get; set; }

        public int ExpiresInSeconds { get; set; }

        public Dictionary<string, string> User { get; set; } = null!;
    }
}
