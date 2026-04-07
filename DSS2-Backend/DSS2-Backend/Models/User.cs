using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [EmailAddress, Required, MaxLength(254)]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public string? DisplayName { get; set; }

        public virtual List<TodoItem> TodoList { get; set; } = new List<TodoItem>();
    }
}
