using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DSS2_Backend.Models
{
    public class TodoItem
    {
        public TodoItem()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Details { get; set; } = null!;

        [Required]
        public Priority Priority { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; private set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = null!;

        [Required]
        public Guid UserId { get; set; }
    }

    public enum Priority { low = 1, medium = 2, high = 3 }
}
