using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS2_Backend.Models
{
    public class TodoItem
    { 
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = null!;

        public string? Details { get; set; }

        public Priority Priority { get; set; }

        public DateTime? DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsPublic { get; set; } = false;

        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }

    public enum Priority { Low = 1, Medium = 2, High = 3 }
}
