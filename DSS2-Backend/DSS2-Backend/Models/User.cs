namespace DSS2_Backend.Models
{
    public class User
    {
        public User()
        {
            this.CreatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public DateTime CreatedAt { get; private set; }
    }
}
