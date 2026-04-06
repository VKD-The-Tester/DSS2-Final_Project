using DSS2_Backend.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class UpdateTodoRequestDto
    {
        [JsonPropertyName("title"), Length(3, 100)]
        public string Title { get; set; } = null!;

        [JsonPropertyName("details"), MaxLength(1_000)]
        public string Details { get; set; } = null!;

        [JsonPropertyName("priority")]
        public Priority Priority { get; set; }

        [JsonPropertyName("dueDate"), DisplayFormat(DataFormatString = "YYYY-MM-DD")]
        public DateTime? DueDate { get; set; }

        [JsonPropertyName("isPublic")]
        public bool IsPublic { get; set; }

        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; }
    }
}
