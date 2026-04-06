using DSS2_Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class TodoResponseDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = null!;

        [JsonPropertyName("details")]
        public string? Details { get; set; }

        [JsonPropertyName("priority")]
        public Priority Priority { get; set; }

        [JsonPropertyName("dueDate")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("isCompleted")]
        public bool IsCompleted {  get; set; }

        [JsonPropertyName("isPublic")]
        public bool IsPublic { get; set; } = false;

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updatedAt")]
        public DateTime? UpdatedAt { get; set; }
    }
}
