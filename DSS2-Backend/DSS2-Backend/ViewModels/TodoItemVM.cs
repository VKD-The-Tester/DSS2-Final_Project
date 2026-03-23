using DSS2_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.ViewModels
{
    public class TodoItemVM
    {
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
    }
}
