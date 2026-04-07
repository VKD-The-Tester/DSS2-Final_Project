using DSS2_Backend.Dtos;
using DSS2_Backend.Models;
using DSS2_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DSS2_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IQueryParamService _queryParam;

        public TodosController(ApplicationDbContext context, IQueryParamService queryParam)
        {
            this._context = context;
            this._queryParam = queryParam;
        }

        [HttpGet("public"), AllowAnonymous]
        public ActionResult<PaginationResponseDto> GetPublicTodos([FromQuery] QueryParamDto request)
        {
            List<TodoItem>? isPublicTodos = _context.Todos.Where(t => t.IsPublic == true).ToList();

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

            // Filtering the todos based on the status.
            todoList = _queryParam.FilterByStatus(todoList, request.Status).ToList();

            // Filtering the todos based on their priority.
            todoList = _queryParam.FilterByPriority(todoList, request.Priority).ToList();

            // Date range operations
            todoList = _queryParam.DueFrom(todoList, request.DueFrom).ToList();

            todoList = _queryParam.DueTo(todoList, request.DueTo).ToList();
            
            // Sorting operations based on the provided query parameters.
            todoList = _queryParam.SortByProperty(todoList, request.SortBy).ToList();

            todoList = _queryParam.SortByDirection(todoList, request.SortDir).ToList();

            // Search Operation
            todoList = _queryParam.Search(todoList, request.Search).ToList();

            // Applying Pagination
            var todosPerPage = _queryParam.ApplyPagination(todoList, request.Page, request.PageSize).ToList();

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
        public ActionResult<List<PaginationResponseDto>> GetUserTodos([FromQuery] QueryParamDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "Validation Failed",
                    Status = 401
                });
            }

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

            // Filtering the todos based on the status.
            todoList = _queryParam.FilterByStatus(todoList, request.Status).ToList();

            // Filtering the todos based on their priority.
            todoList = _queryParam.FilterByPriority(todoList, request.Priority).ToList();

            // Date range operations
            todoList = _queryParam.DueFrom(todoList, request.DueFrom).ToList();

            todoList = _queryParam.DueTo(todoList, request.DueTo).ToList();

            // Sorting operations based on the provided query parameters.
            todoList = _queryParam.SortByProperty(todoList, request.SortBy).ToList();

            todoList = _queryParam.SortByDirection(todoList, request.SortDir).ToList();

            // Search Operation
            todoList = _queryParam.Search(todoList, request.Search).ToList();

            // Applying Pagination
            var todosPerPage = _queryParam.ApplyPagination(todoList, request.Page, request.PageSize).ToList();

            var response = new PaginationResponseDto
            {
                Items = todosPerPage,
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "Validation Failed",
                    Status = 401
                });
            }

            var todo = new TodoItem
            {
                Title = request.Title,
                Details = request.Details,
                Priority = request.Priority,
                DueDate = request.DueDate,
                IsPublic = request.IsPublic,
                UserId = Guid.Parse(userId)
            };

            await _context.Todos.AddAsync(todo);

            await _context.SaveChangesAsync();

            return Created();
        }

        [HttpGet("{id:guid}"), Authorize]
        public IActionResult GetTodoById(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "Validation Failed",
                    Status = 401
                });
            }

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "Validation Failed",
                    Status = 401
                });
            }

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "Validation Failed",
                    Status = 401
                });
            }

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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new ProblemDetails
                {
                    Type = "https://httpstatuses.com/401",
                    Title = "Validation Failed",
                    Status = 401
                });
            }

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
