using DSS2_Backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class CreateTodoRequestDto
    {
        [JsonPropertyName("title"), Length(3, 100)]
        public string Title { get; set; } = null!;

        [JsonPropertyName("details"), MaxLength(1_000)]
        public string? Details { get; set; }

        [Required, JsonPropertyName("priority")]
        public Priority Priority { get; set; }

        [JsonPropertyName("dueDate")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("isPublic")]
        public bool IsPublic { get; set; } = false;
    }
}
