using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS2_Backend.Models
{
    public class User
    {
        public User()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [EmailAddress(ErrorMessage = "The email format is invalid."), Required]
        public required string Email { get; set; } = null!;

        [Required]
        public required string PasswordHash { get; set; } = null!;

        [DataType(DataType.DateTime), Required]
        public DateTime CreatedAt { get; private set; }

        public string? DisplayName { get; set; } = string.Empty;

        public virtual List<TodoItem>? TodoList { get; set; } = new List<TodoItem>();
    }
}
