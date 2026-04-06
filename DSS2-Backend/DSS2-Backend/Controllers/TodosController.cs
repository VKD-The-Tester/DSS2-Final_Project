using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodosController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("public")]
        public async Task<ActionResult<PaginationResponseDto>> GetPublicTodos([FromQuery] ListingDto request)
        {
            var isPublicTodos = _context.Todos.Where(t => t.IsPublic == true).ToList();

            List<TodoResponseDto> todoList = new List<TodoResponseDto>();

            foreach (var t in isPublicTodos)
            {
                todoList.Add(new TodoResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Details = t.Details,
                    Priority = t.Priority,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    IsPublic = t.IsPublic,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                });
            }

            // Filtering operations based on the provided query parameters.
            if (request.Status == "active")
            {
                todoList = todoList.Where(t => t.IsCompleted == false).ToList();
            }

            if (request.Status == "completed")
            {
                todoList = todoList.Where(t => t.IsCompleted == true).ToList();
            }

            switch (request.Priority)
            {
                case "low":
                    todoList = todoList.Where(t => t.Priority == Priority.Low).ToList();
                    break;
                case "medium":
                    todoList = todoList.Where(t => t.Priority == Priority.Medium).ToList();
                    break;
                case "high":
                    todoList = todoList.Where(t => t.Priority == Priority.High).ToList();
                    break;
                default:
                    break;
            }

            // Date range operations
            if (request.DueFrom != null)
            {
                todoList = todoList.Where(t => t.DueDate >= request.DueFrom).ToList();
            }

            if (request.DueTo != null)
            {
                todoList = todoList.Where(t => t.DueDate <= request.DueTo).ToList();
            }

            // Sorting operations based on the provided query parameters.
            switch (request.SortBy)
            {
                case "dueDate":
                    todoList = todoList.OrderBy(t => t.DueDate).ToList();
                    break;
                case "priority":
                    todoList = todoList.OrderBy(t => t.Priority).ToList();
                    break;
                case "title":
                    todoList = todoList.OrderBy(t => t.Title).ToList();
                    break;
                default:
                    break;
            }

            if (request.SortDir == "asc")
            {
                todoList.OrderBy(t => t);
            }

            // Search Operation
            if (request.Search != null)
            {
                todoList = (List<TodoResponseDto>)todoList.Where(t => t.Title.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }

            // Applying Pagination
            var todosPerPage = todoList.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();

            var response = new PaginationResponseDto
            {
                Items = todosPerPage,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = todoList.Count,
                TotalPages = (int) Math.Ceiling((decimal)todoList.Count / request.PageSize)
            };

            return Ok(response);
        }

        [HttpGet, Authorize]
        public ActionResult<List<TodoResponseDto>> GetUserTodos([FromQuery] ListingDto request)
        {
            var isPrivateTodos = _context.Todos.Where(t => t.IsPublic == false).ToList();

            List<TodoResponseDto> todoList = new List<TodoResponseDto>();

            foreach (var t in isPrivateTodos)
            {
                todoList.Add(new TodoResponseDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Details = t.Details,
                    Priority = t.Priority,
                    DueDate = t.DueDate,
                    IsCompleted = t.IsCompleted,
                    IsPublic = t.IsPublic,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt
                });
            }

            // Filtering operations based on the provided query parameters.
            if (request.Status == "active")
            {
                todoList = todoList.Where(t => t.IsCompleted == false).ToList();   
            }

            if (request.Status == "completed")
            {
                todoList = todoList.Where(t => t.IsCompleted == true).ToList();
            }

            switch (request.Priority)
            {
                case "low":
                    todoList = todoList.Where(t => t.Priority == Priority.Low).ToList();
                    break;
                case "medium":
                    todoList = todoList.Where(t => t.Priority == Priority.Medium).ToList();
                    break;
                case "high":
                    todoList = todoList.Where(t => t.Priority == Priority.High).ToList();
                    break;
                default:
                    break;
            }

            // Date range operations
            if (request.DueFrom != null) 
            { 
                todoList = todoList.Where(t => t.DueDate >=  request.DueFrom).ToList();
            }

            if (request.DueTo != null) 
            { 
                todoList = todoList.Where(t => t.DueDate <= request.DueTo).ToList();
            }

            // Sorting operations based on the provided query parameters.
            switch (request.SortBy)
            {
                case "dueDate":
                    todoList = todoList.OrderBy(t => t.DueDate).ToList();
                    break;
                case "priority":
                    todoList = todoList.OrderBy(t => t.Priority).ToList();
                    break;
                case "title":
                    todoList = todoList.OrderBy(t => t.Title).ToList(); 
                    break;
                default : 
                    break;
            }

            if (request.SortDir == "asc")
            {
                todoList.OrderBy(t => t);
            }

            // Search Operation
            if (request.Search != null) 
            {
                todoList = (List<TodoResponseDto>)todoList.Where(t => t.Title.Contains(request.Search, StringComparison.OrdinalIgnoreCase));
            }


            var response = new PaginationResponseDto
            {
                Items = todoList,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalItems = todoList.Count,
                TotalPages = (int)Math.Ceiling((decimal)todoList.Count / request.PageSize)
            };

          
            return Ok(response);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreateTodo(CreateTodoRequestDto request)
        {
            var todo = new TodoItem
            {
                Title = request.Title,
                Details = request.Details,
                Priority = request.Priority,
                DueDate = request.DueDate,
                IsPublic = request.IsPublic,
            };

            await _context.Todos.AddAsync(todo);

            await _context.SaveChangesAsync();

            return Created();
        }

        [HttpGet("{id:guid}"), Authorize]
        public IActionResult GetTodoById(Guid id)
        {
            var existingTodo = _context.Todos.FirstOrDefault(t => t.Id == id);

            if (existingTodo == null)
            {

                return NotFound(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/404",
                    Title = "Validation Failed",
                    Status = 404
                });
            }

            var response = new TodoResponseDto
            {
                Id = id,
                Title = existingTodo.Title,
                Priority = existingTodo.Priority,
                DueDate = existingTodo.DueDate,
                IsCompleted = existingTodo.IsCompleted,
                IsPublic = existingTodo.IsPublic,
                CreatedAt = existingTodo.CreatedAt,
                UpdatedAt = existingTodo.UpdatedAt
            };

            return Ok(response);
        }

        
        [HttpPut("{id:guid}"), Authorize]
        public async Task<IActionResult> UpdateUserTodo(Guid id, UpdateTodoRequestDto request) 
        {
            var todoExists = await _context.Todos.FindAsync(id);

            if (todoExists == null) 
            {
                return NotFound(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/404",
                    Title = "Validation Failed",
                    Status = 404
                });
            }

            todoExists.Title = request.Title;
            todoExists.Details = request.Details;
            todoExists.Priority = request.Priority;
            todoExists.DueDate = request.DueDate;
            todoExists.IsPublic = request.IsPublic;
            todoExists.IsCompleted = request.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id:guid}/completion"), Authorize]
        public async Task<IActionResult> UpdateIsCompleted(Guid id, SetCompletionRequestDto setCompletion) 
        { 
            var todoExists = await _context.FindAsync<TodoItem>(id);

            if (todoExists == null) 
            {
                return NotFound(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/404",
                    Title = "Validation Failed",
                    Status = 404
                });
            }

            todoExists.IsCompleted = setCompletion.IsCompleted;

            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id:guid}"), Authorize]
        public async Task<IActionResult> DeleteUserTodo(Guid id) 
        {
            var todoExists = await _context.Todos.FindAsync(id);

            if (todoExists == null) 
            {
                return NotFound(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/404",
                    Title = "Validation Failed",
                    Status = 404
                });
            }

            return NoContent();
        }
    }
}
