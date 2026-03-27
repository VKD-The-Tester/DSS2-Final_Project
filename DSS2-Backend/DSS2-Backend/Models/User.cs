using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS2_Backend.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [EmailAddress(ErrorMessage = "The email format is invalid."), Required]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; }

        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public string? DisplayName { get; set; } = null;

        public virtual List<TodoItem> TodoList { get; set; } = new List<TodoItem>();
    }
}
