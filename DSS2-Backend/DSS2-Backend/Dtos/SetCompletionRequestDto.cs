using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class SetCompletionRequestDto
    {
        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
    }
}
