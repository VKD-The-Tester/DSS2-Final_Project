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
        public int Id { get; set; }

        [EmailAddress(ErrorMessage = "The email format is invalid."), Required]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        [DataType(DataType.DateTime), Required]
        public DateTime CreatedAt { get; private set; }

        public virtual List<TodoItem>? TodoList { get; set; }
    }
}
