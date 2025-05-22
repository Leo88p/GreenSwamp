using System;

namespace Lab3.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Username { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? IsActive { get; set; }
    }
}
