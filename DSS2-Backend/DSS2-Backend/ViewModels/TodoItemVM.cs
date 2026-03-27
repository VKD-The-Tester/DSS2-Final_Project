using DSS2_Backend.Models;
using System.ComponentModel.DataAnnotations;

namespace DSS2_Backend.ViewModels
{
    public class TodoItemVM
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public required string Details { get; set; }

        [Required]
        public Priority Priority { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [Required]
        public DateTime? UpdatedAt { get; set; }
    }
}
