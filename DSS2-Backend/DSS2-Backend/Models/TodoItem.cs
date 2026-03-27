using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS2_Backend.Models
{
    public class TodoItem
    { 
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Details { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;

        [Required]
        public bool IsPublic { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        [Required]
        public DateTime? UpdatedAt { get; set; } = null;

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;
    }

    public enum Priority { low = 1, medium = 2, high = 3 }
}
