namespace DSS2_Backend.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Details { get; set; } = null!;

        public Priority Priority { get; set; }

        public string DueDate { get; set; } = null!;

        public bool IsCompleted { get; set; }

        public bool IsPublic { get; set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; set; }
    }

    public enum Priority { low = 1, medium = 2, high = 3 }
}
