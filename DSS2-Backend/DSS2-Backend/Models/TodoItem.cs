namespace DSS2_Backend.Models
{
    public class TodoItem
    {
        public TodoItem()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Details { get; set; } = null!;

        public Priority Priority { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; set; }

        public virtual User User { get; set; } = null!;

        public int UserId { get; set; }
    }

    public enum Priority { low = 1, medium = 2, high = 3 }
}
