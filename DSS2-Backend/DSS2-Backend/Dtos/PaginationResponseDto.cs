using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class PaginationResponseDto
    {
        [JsonPropertyName("items")]
        public List<TodoResponseDto>? Items { get; set; }

        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        [JsonPropertyName("totalItems")]
        public int TotalItems { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPages { get; set; }
    }
}
