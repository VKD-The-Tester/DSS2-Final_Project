using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DSS2_Backend.Dtos
{
    public class QueryParamDto
    {
        [JsonPropertyName("page"), Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [JsonPropertyName("pageSize"), Range(1, 50)]
        public int PageSize { get; set; } = 10;

        [JsonPropertyName("status"), RegularExpression("^(all|active|completed)$")]
        public string Status { get; set; } = "all";

        [JsonPropertyName("priority"), RegularExpression("^(low|medium|high)$")]
        public string? Priority { get; set; }

        [JsonPropertyName("dueFrom")]
        public DateTime? DueFrom { get; set; }

        [JsonPropertyName("dueTo")]
        public DateTime? DueTo { get; set; }

        [JsonPropertyName("sortBy"), RegularExpression("^(createdAt|dueDate|priority|title)$")]
        public string SortBy { get; set; } = "createdAt";

        [JsonPropertyName("sortDir"), RegularExpression("^(asc|desc)$")]
        public string SortDir { get; set; } = "desc";

        [JsonPropertyName("search"), MaxLength(100)]
        public string? Search { get; set; }
    }
}
