using DSS2_Backend.Dtos;
using DSS2_Backend.Models;

namespace DSS2_Backend.Services
{
    public class QueryParamService : IQueryParamService
    {
        public IEnumerable<TodoResponseDto> FilterByStatus(List<TodoResponseDto> todos, string status)
        {

            // Filtering operations based on the provided query parameters.
            if (status == "active")
            {
                todos = todos.Where(t => t.IsCompleted == false).ToList();
                return todos;
            }

            if (status == "completed")
            {
                todos = todos.Where(t => t.IsCompleted == true).ToList();
                return todos;
            }

            return todos;
        }

        public IEnumerable<TodoResponseDto> FilterByPriority(List<TodoResponseDto> todos, string? priority)
        {
            switch (priority)
            {
                case "low":
                    todos = todos.Where(t => t.Priority == Priority.Low).ToList();
                    return todos;
                case "medium":
                    todos = todos.Where(t => t.Priority == Priority.Medium).ToList();
                    return todos;
                case "high":
                    todos = todos.Where(t => t.Priority == Priority.High).ToList();
                    return todos;
                default:
                    break;
            }

            return todos;
        }

        public IEnumerable<TodoResponseDto> DueFrom(List<TodoResponseDto> todos, DateTime? dueFrom)
        {
            if(dueFrom != null)
            {
                todos = todos.Where(t => t.DueDate >= dueFrom).ToList();
                return todos;
            }
            
            return todos;
        }

        public IEnumerable<TodoResponseDto> DueTo(List<TodoResponseDto> todos, DateTime? dueTo)
        {
            if (dueTo != null)
            {
                todos = todos.Where(t => t.DueDate <= dueTo).ToList();
                return todos;
            }

            return todos;
        }

        public IEnumerable<TodoResponseDto> SortByProperty(List<TodoResponseDto> todos, string? property)
        {
            switch (property)
            {
                case "dueDate":
                    todos = todos.OrderBy(t => t.DueDate).ToList();
                    return todos;
                case "priority":
                    todos = todos.OrderBy(t => t.Priority).ToList();
                    return todos;
                case "title":
                    todos = todos.OrderBy(t => t.Title).ToList();
                    return todos;
                default:
                    break;
            }

            return todos;
        }

        public IEnumerable<TodoResponseDto> SortByDirection(List<TodoResponseDto> todos, string? direction)
        {
            if (direction == "asc")
            {
                todos = todos.OrderBy(t => t).ToList();
                return todos;
            }

            return todos;
        }

        public IEnumerable<TodoResponseDto> Search(List<TodoResponseDto> todos, string? wildCard)
        {
            if (wildCard != null)
            {
                todos = todos.Where(t => t.Title.Contains(wildCard, StringComparison.OrdinalIgnoreCase)).ToList();
                return todos;
            }

            return todos;
        }

        public IEnumerable<TodoResponseDto> ApplyPagination(List<TodoResponseDto> todos, int page, int pageSize)
        {
            var todosPerPage = todos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return todosPerPage;
        }
    }
}
