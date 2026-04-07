using DSS2_Backend.Dtos;
using System.Collections;

namespace DSS2_Backend.Services
{
    public interface IQueryParamService
    {
        public IEnumerable<TodoResponseDto> FilterByStatus(List<TodoResponseDto> todos, string status);

        public IEnumerable<TodoResponseDto> FilterByPriority(List<TodoResponseDto> todos, string? priority);

        public IEnumerable<TodoResponseDto> SortByProperty(List<TodoResponseDto> todos, string? property);

        public IEnumerable<TodoResponseDto> SortByDirection(List<TodoResponseDto> todos, string? direction);

        public IEnumerable<TodoResponseDto> Search(List<TodoResponseDto> todos, string? wildCard);

        public IEnumerable<TodoResponseDto> DueFrom(List<TodoResponseDto> todos, DateTime? dueFrom);

        public IEnumerable<TodoResponseDto> DueTo(List<TodoResponseDto> todos, DateTime? dueTo);

        public IEnumerable<TodoResponseDto> ApplyPagination(List<TodoResponseDto> todos, int page, int pageSize);
    }
}
